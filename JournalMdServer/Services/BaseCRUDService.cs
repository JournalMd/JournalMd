﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JournalMdServer.Repositories;
using JournalMdServer.Models;
using JournalMdServer.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using JournalMdServer.DTOs.Notes;
using AutoMapper;
using JournalMdServer.Helpers;
using System.Web.Http.ModelBinding;
using Task = System.Threading.Tasks.Task;

namespace JournalMdServer.Services
{
    public class BaseCRUDService<TEntity, INP, OUTP> : IBaseCRUDService<INP, OUTP> where TEntity : BaseAuditEntity
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        
        public BaseCRUDService(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<OUTP>>> GetAll(long userId)
        {
            var entities = await _repository.Query.Where(m => m.UserId == userId).ToListAsync();
            return _mapper.Map<List<OUTP>>(entities);
            //var notes = _repository.Query.GetPaged(1, 3);
            //return _mapper.Map<PagedResult<NoteOutput>>(notes);
        }

        public async Task<OUTP> GetById(long id, long userId)
        {
            var entitie = await _repository.Query.FirstOrDefaultAsync(m => m.UserId == userId && m.Id == id);
            return _mapper.Map<OUTP>(entitie);
        }

        public async Task<OUTP> Create(INP inputModel, long userId)
        {
            var entry = _mapper.Map<TEntity>(inputModel);
            entry.UserId = userId;
            entry.CreatedById = userId;
            entry.CreatedAt = DateTime.Now;
            entry.UpdatedById = userId;
            entry.UpdatedAt = DateTime.Now;

            _repository.Insert(entry);
            await _repository.Context.SaveChangesAsync();

            return _mapper.Map<OUTP>(entry);
        }

        public async Task Update(long id, INP inputModel, long userId)
        {
            var dbEntry = await _repository.Query.FirstOrDefaultAsync(m => m.UserId == userId && m.Id == id);

            if (id != dbEntry.Id)
                throw new ArgumentException("Invalid id");

            dbEntry = _mapper.Map<INP, TEntity>(inputModel, dbEntry);

            _repository.Update(dbEntry);
            await _repository.Context.SaveChangesAsync();
        }

        public async Task<long?> Delete(long id, long userId)
        {
            var dbEntry = await _repository.Query.FirstOrDefaultAsync(m => m.UserId == userId && m.Id == id);

            if (dbEntry == null)
                return null;

            _repository.Delete(dbEntry);
            await _repository.Context.SaveChangesAsync();

            return id;
        }

        /*private bool ItemExists(long id, long userId)
        {
            return _repository.Query.Any(m => m.UserId == userId && m.Id == id);
        }*/
    }
}
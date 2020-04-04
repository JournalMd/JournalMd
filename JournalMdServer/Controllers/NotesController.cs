using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JournalMdServer.Models;
using JournalMdServer.Services;
using JournalMdServer.Repositories;
using JournalMdServer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using JournalMdServer.DTOs.Notes;
using JournalMdServer.Helpers;
using JournalMdServer.DTOs;

namespace JournalMdServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : BaseCRUDController<NotesService, NoteInput, NoteOutput>
    {
        public NotesController(NotesService service): base(service)
        {
        }

        // Intentionally left empty
    }
}

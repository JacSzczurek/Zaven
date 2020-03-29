using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZavenDotNetInterview.Core.Models;
using ZavenDotNetInterview.Services.Interfaces;
using ZavenDotNetInterview.Services.Validators;

namespace ZavenDotNetInterview.App.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobProcessorService _jobProcessorService;
        private readonly IJobService _jobService;
        private readonly IJobValidator _jobValidator;
        public JobsController(IJobProcessorService jobProcessorService, IJobService jobService, IJobValidator jobValidator)
        {
            _jobProcessorService = jobProcessorService;
            _jobService = jobService ?? throw new ArgumentNullException(nameof(jobService));
            _jobValidator = jobValidator ?? throw new ArgumentNullException(nameof(jobValidator));
        }

        // GET: Jobs
        public ActionResult Index()
        {
            var allJobs = _jobService.GetAllJobs();

            return View(allJobs);
        }

        // POST: Jobs/Process
        [HttpGet]
        public ActionResult Process()
        {
            _jobProcessorService.ProcessJobs();

            return RedirectToAction("Index");
        }

        // GET: Jobs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        [HttpPost]
        public ActionResult Create(CreateJobRequest request)
        {
            if(_jobValidator.IsValid(request, out var validationKeys))
            {
                _jobService.CreateJob(request);
                return View();
            }

            BuildModelState(validationKeys);
            return View();
        }

        // GET: Jobs/Details
        [HttpGet]
        public ActionResult Details(int jobId)
        {
            var jobDetails = _jobService.GetJob(x => x.Id == jobId);
            if (jobDetails == null)
                return HttpNotFound();

            return View(jobDetails);
        }

        private void BuildModelState(Dictionary<string, string> validationKeys)
        {
            foreach (var item in validationKeys)
            {
                ModelState.AddModelError(item.Key, item.Value);
            }
        }
    }
}

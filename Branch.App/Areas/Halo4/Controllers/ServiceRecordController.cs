﻿using System.Web.Mvc;
using Branch.App.Areas.Halo4.Filters;
using Branch.App.Areas.Halo4.Models;
using Branch.Models.Services.Halo4._343.Responses;

namespace Branch.App.Areas.Halo4.Controllers
{
	public class ServiceRecordController : Controller
	{
		//
		// GET: /Halo4/ServiceRecord/{gamertag}
		[ValidateH4ServiceRecordFilter]
		[ValidateH4ApiStatus]
		public ActionResult Index(string gamertag, ServiceRecord serviceRecord)
		{
			return View(new ServiceRecordViewModel(serviceRecord));
		}
	}
}

using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreIdentityCustom.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult Review()
        {
            return View();
        }

       
        [HttpPost]
        public IActionResult SubmitApplicationReview(Review model)
        {
            if (ModelState.IsValid)
            {
                return View("ReviewConfirmation");
            }

            
            return View("Review", model);
        }


        public IActionResult ReviewConfirmation()
        {
            return View();
        }
    }
}

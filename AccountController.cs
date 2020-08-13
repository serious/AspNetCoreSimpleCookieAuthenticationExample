public class AccountController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            User result = validate(username, password);
            if (result != null)
            {
                HttpContext.Session.Clear();
                List<Claim> userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()),  //NameIdentifier is required
                    new Claim(ClaimTypes.Name, result.Name),
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties());
                return RedirectToAction("Index", "Home");
            }

            else
            {
                ViewBag.Error = "Username or Password is wrong";
                return View();
            }
        }



        public async Task<IActionResult> Logout()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                HttpContext.Session.Clear();
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            return RedirectToAction(nameof(AccountController.Login));
        }
    }

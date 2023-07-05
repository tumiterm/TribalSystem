
using CMS.App_Start;
using CMS.Helpers;
using CMS.Mail_Config;
using CMS.Models.DAL;
using CMS.Models.Repositories;
using CMS.Models.Repositories.Db;
using CMS.Models.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CMS.Controllers
{
    public class AuthController : Controller
    {

        private IUserRepository<User> _userRepository;
        public AuthController()
        {
            this._userRepository = new UserRepository(new CMSEntities());

        }

        [Authorize(Roles= "Administrator")]
        public ActionResult ControlCentre()
        {
            return View();
        }



        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            string message = "";
            bool status = false;

            if (string.IsNullOrEmpty(email))
            {
                message = "Error: Email is a required field!";
            }
            else
            {
                using (CMSEntities db = new CMSEntities())
                {
                    var account = db.Users.Where(a => a.Email == email).FirstOrDefault();

                    if (account != null)
                    {
                        string resetCode = Guid.NewGuid().ToString();

                        SendVerificationLinkEmail(account.Email, resetCode, "PasswordReset");

                        account.ResetPasswordCode = resetCode;

                        db.Configuration.ValidateOnSaveEnabled = false;

                        db.SaveChanges();

                        message = "Reset password link successfully sent to your email.";
                    }
                    else
                    {
                        message = "Error: Account not found!";
                    }
                }

            }

            ViewBag.Message = message;

            return View();
        }

        public ActionResult UserSignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserSignUp([Bind(Exclude = "IsEmailVerified,ActivationCode,ResetPasswordCode")] User user)
        {
            bool Status = false;
            string message = "";

            if (ModelState.IsValid)
            {

                #region 
 
                //var doesExist = await _userRepository.IsValidMail(user.Email);

                //if (doesExist)
                //{
                //    ModelState.AddModelError("EmailExist", "Email already exist");
                //    return View();

                //}

                #endregion

                #region Generate Activation Code 
                user.ActivationCode = Helper.GenerateGuid();
                user.Id = Helper.GenerateGuid();
                #endregion

                #region  Password Hashing 
                user.Password = Helper.EncryptInput(user.Password);
                user.ConfirmPassword = Helper.EncryptInput(user.ConfirmPassword);
                #endregion
                user.IsEmailVerified = false;

                #region Save to Database
         
                
                    try
                    {
                        _userRepository.InsertRecordAsync(user);

                        await _userRepository.SaveAsync();
                        
                        TempData["successMessage"] = $"User Successfully Registered";

                        SendVerificationLinkEmail(user.Email, user.ActivationCode.ToString());
                        message = " Registration successfully done. Account activation link " +
                            " has been sent to your email: " + user.Email + "\nKindly click on the link to activate your account.\n" +
                            " Yours In-Community";
                        Status = true;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    

                }
                #endregion
            }
            else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;

            return View(user);
        }

        [HttpGet]
        public ActionResult UserSignIn()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserSignIn(UserLogin user, string ReturnUrl = "")
        {

            if (!string.IsNullOrEmpty(user.EmailID))
            {
                if (!string.IsNullOrEmpty(user.Password))
                {
                    if (ModelState.IsValid)
                    {
                        string message = "";

                        using (CMSEntities dc = new CMSEntities())
                        {
                            //var userData =  dc.Users.Where(a => a.Email == user.EmailID).FirstOrDefault();
                            var userData = await _userRepository.OnGetUser(user.EmailID);

                            if (userData != null)
                            {
                                if (!userData.IsEmailVerified)
                                {
                                    ViewBag.Message = "Please verify your email first";

                                    return View();
                                }

                                if (string.Compare(Helper.EncryptInput(user.Password), userData.Password) == 0)
                                {
                                    int timeout = user.RememberMe ? 525600 : 20;

                                    var ticket = new FormsAuthenticationTicket(user.EmailID, user.RememberMe, timeout);

                                    string encrypted = FormsAuthentication.Encrypt(ticket);

                                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);

                                    cookie.Expires = DateTime.Now.AddMinutes(timeout);

                                    cookie.HttpOnly = true;

                                    Response.Cookies.Add(cookie);

                                    if (Url.IsLocalUrl(ReturnUrl))
                                    {
                                        return Redirect(ReturnUrl);
                                    }
                                    else
                                    {
                                        Session["FirstName"] = userData.Name;

                                        Session["LastName"] = userData.LastName;

                                        Session["Phone"] = userData.Phone;

                                        Session["Email"] = userData.Email;



                                        //Analytics
                                        //Session["AppointmentCount"] = dc.PatientAppointments.ToList().Count();
                                        //Session["UserCount"] = dc.Users.ToList().Count();
                                        //Session["PatientCount"] = dc.Patients.ToList().Count();
                                        //Session["StockCount"] = dc.Inventories.ToList().Count();
                                        //Session["CommentCount"] = dc.Comments.ToList().Count();
                                        //Session["PractitionerCount"] = dc.MedicalPractitioners.ToList().Count();
                                        //Session["ClinicCount"] = dc.HealthClinics.ToList().Count();

                                        return RedirectToAction("ControlCentre", "Auth");

                                    }
                                }
                                else
                                {
                                    message = "Invalid credentials provided";
                                }
                            }
                            else
                            {
                                message = "Invalid credentials provided";
                            }
                        }
                        ViewBag.Message = message;

                        return View();
                    }

                }
                else
                {
                    ViewBag.Message = "Error: Password is a required field!";
                }
            }
            else
            {
                ViewBag.Message = "Error: Username/Email is a required field!";

            }

            return View();
        }

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;

            using (CMSEntities dc = new CMSEntities())
            {
                dc.Configuration.ValidateOnSaveEnabled = false;

                var v = dc.Users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();

                if (v != null)
                {
                    v.IsEmailVerified = true;

                    dc.SaveChanges();

                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Request Not Valid!";
                }
            }
            ViewBag.Status = Status;

            return View();
        }

        public ActionResult PasswordReset(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }

            using (CMSEntities db = new CMSEntities())
            {
                var user = db.Users.Where(a => a.ResetPasswordCode == id).FirstOrDefault();

                if (user != null)
                {
                    PasswordResetViewModel model = new PasswordResetViewModel();

                    model.ResetCode = id;

                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PasswordReset(PasswordResetViewModel pass)
        {
            var message = "";

            if (ModelState.IsValid)
            {
                using (CMSEntities dc = new CMSEntities())
                {
                    var user = dc.Users.Where(a => a.ResetPasswordCode == pass.ResetCode).FirstOrDefault();

                    if (user != null)
                    {
                        user.Password = Helper.EncryptInput(pass.NewPassword);

                        user.ResetPasswordCode = "";

                        dc.Configuration.ValidateOnSaveEnabled = false;

                        dc.SaveChanges();

                        message = "New password updated successfully";
                    }
                }
            }
            else
            {
                message = "Error!";
            }
            ViewBag.Message = message;
            return View(pass);
        }

        [NonAction]
        public ActionResult SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/Auth/" + emailFor + "/" + activationCode;

            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress(EmailInfo.Username, "Tribal System");

            var toEmail = new MailAddress(emailID);

            var fromEmailPassword = EmailInfo.Password;

            string subject = "";

            string body = "";

            if (emailFor == "VerifyAccount")
            {
                subject = "Registration Confirmation!";

                body = "<br/><br/> Account successfully created" +
                   " thanks for registering with us. Please click on the below link to verify your account" +
                   " <br/><br/><a href='" + link + "'>" + link + "</a> ";

            }
            else if (emailFor == "PasswordReset")
            {
                subject = "Reset Password";
                body = "Need to reset your password? No problem! Just click the button</b> " +
                    "below to and you'll be good to go. If not - kindly ignore this email.<br/><br/><a href=" + link + ">Reset Password link</a>";
            }

            var smtp = new SmtpClient
            {
                Host = EmailInfo.Smtp_Host,

                Port = Convert.ToInt32(EmailInfo.Smtp_Port),

                EnableSsl = true,

                DeliveryMethod = SmtpDeliveryMethod.Network,

                UseDefaultCredentials = false,

                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,

                Body = body,

                IsBodyHtml = true
            })

                smtp.Send(message);

            return View();
        }

        public ActionResult UserSignOut()
        {
            FormsAuthentication.SignOut();

            Session.Clear();
            //Session.RemoveAll();
            //Session.Abandon();

            return RedirectToAction("UserSignIn");
        }

    }
}
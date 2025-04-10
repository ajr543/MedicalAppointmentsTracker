namespace MedicalAppointmentsTracker.Models
{
    public class CustomAuthorization
    {
        public static bool IsUserAuthorized(HttpContext httpContext)
        {
            var user = httpContext.Session.GetString("UserData");
            return user != null; 
        }
    }

}

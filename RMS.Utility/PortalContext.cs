namespace RMS.Utility
{
   public class PortalContext
    {
       public static PortalUser CurrentUser
       {
           get { var value = CustomSession.GetValue(SessionKey.PortalUser); return value != null ? (PortalUser)value : new PortalUser(); }
           set { CustomSession.SetValue(SessionKey.PortalUser, value); }
       }
    }
}

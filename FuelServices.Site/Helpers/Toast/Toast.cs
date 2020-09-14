namespace FuelServices.Site.Helpers.Toast
{
    public enum ToasterType
    {
        primary = 0,
        info = 1,
        error = 2,
        success = 3,
        warning = 4
    }

    public class Toast
    {
        public string Message { get; set; }
        public string Type { get; set; }
        public string Position = "top-right";

        public Toast()
        {
        }

        public Toast(string message, ToasterType type, bool isFront = false)
        {
            Message = message;
            Type = type.ToString();
            if (isFront)
                Position = "bottom-left";
        }

        public Toast(string message, ToasterType type, string position)
        {
            Message = message;
            Type = type.ToString();
            Position = position;
        }

        public static Toast SucsessToast(string msg = "Operation accomplished successfully") =>
            new Toast(msg, ToasterType.success);

        public static Toast ErrorToast(string exceptionMessage = "An Error has occured. Try again later.") =>
            new Toast(exceptionMessage, ToasterType.error);

        public static Toast SucsessToastFront(string msg = "Operation accomplished successfully") =>
            new Toast(msg, ToasterType.success, true);

        public static Toast ErrorToastFront(string exceptionMessage = "An Error has occured. Try again later.") =>
            new Toast(exceptionMessage, ToasterType.error, true);
    }
}
namespace UmotaRedEye.Models.Dto
{
    public class BaseViewModel
    {
        public string ErrorMessage { get; set; }
        public bool IsSuccess
        {
            get
            {
                return string.IsNullOrEmpty(ErrorMessage);
            }
        }

    }
}

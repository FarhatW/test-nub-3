namespace jce.Common.Resources.Consent
{
    public class ProcessConsentResult
    {
        public bool IsRedirect => RedirectUri != null;
        public string RedirectUri { get; set; }

        public bool ShowView => ViewModel != null;
        public ConsentResource ViewModel { get; set; }

        public bool HasValidationError => ValidationError != null;
        public string ValidationError { get; set; }
    }
}

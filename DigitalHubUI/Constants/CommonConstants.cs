namespace DigitalHubUI.Constants;

public static class CommonConstants
{
    public static class Taxonomies
    {
        public const string ArticlesCategoriesRootTaxonomyId = "e5cd6d69-1543-427b-ad62-688a99f5e7d4";
        public const string CoursesCategoriesRootTaxonomyId = "ed0e7e9f-e6f8-4939-96bc-1b3489f096ec";
        public const string TagsRootTaxonomyId = "cb0f3a19-a211-48a7-88ec-77495c0f5374";
    }

    // Name of contributors role in Sitefinity
    public const string ContributorsRoleName = "Contributors";
    public const string AdministratorsRoleName = "Administrators";

    public const char LikeSeparator = '|';
    public const string TimestampFormat = "yyyyMMddHHmmssffff";

    // Articles constants
    public static class Articles
    {
        public const string ContentProvider = "digitalhubblogs";
        public const string ContentType = "BlogPost";
    }

    // PublishingStatus taxonomy ID is hardcoded here too
    // If we have another backend, we have to fetch it
    public static class PublishingStatus
    {
        public const string Published = "f0456f1c-55cc-44d7-8d2a-7c1750671166";
        public const string Draft = "e9efa319-b449-4122-84bd-fcf0560970c6";
        public const string UnderReview = "d3c1e50a-1113-4678-9917-5e859b6db703";
    }

    // Courses constants
    public static class Courses
    {
        public const string ContentProvider = "OpenAccessProvider";
        public const string ContentType = "Course";
    }
    
    public static class CoursePublishingStatus
    {
        public const string Published = "5780896d-9544-4c44-bf9a-927510d55dbc";
        public const string Archived = "d06bb8a5-fdac-46e4-8fe6-947e752e4d15";
        public const string Draft = "419d4e4f-5357-43ae-a0f1-39a056e7a4db";
    }

    public static class EnrolmentStatus
    {
        public static readonly (string Number, string Text) Enrolled = (
            Number: "1",
            Text: "Enrolled"
        );

        public static readonly (string Number, string Text) Completed = (
            Number: "2",
            Text: "Completed"
        );

        public static readonly (string Number, string Text) Withdrawn = (
            Number: "4",
            Text: "Withdrawn"
        );

        public static string GetTextByNumber(string number)
        {
            if (Enrolled.Number.Equals(number))
            {
                return Enrolled.Text;
            }

            if (Completed.Number.Equals(number))
            {
                return Completed.Text;
            }

            if (Withdrawn.Number.Equals(number))
            {
                return Withdrawn.Text;
            }

            throw new NotSupportedException($"Enrollment Status index {number} is not supported");
        }
    }

    public static class VerificationStatus
    {
        public static readonly (string Number, string Text) Unverified = (
            Number: "1",
            Text: "Unverified"
        );

        public static readonly (string Number, string Text) Pending = (
            Number: "2",
            Text: "Pending"
        );

        public static readonly (string Number, string Text) Verified = (
            Number: "4",
            Text: "Verified"
        );

        public static readonly (string Number, string Text) Rejected = (
            Number: "8",
            Text: "Rejected"
        );

        public static string GetTextByNumber(string number)
        {
            if (Unverified.Number.Equals(number))
            {
                return Unverified.Text;
            }

            if (Pending.Number.Equals(number))
            {
                return Pending.Text;
            }

            if (Verified.Number.Equals(number))
            {
                return Verified.Text;
            }

            if (Rejected.Number.Equals(number))
            {
                return Rejected.Text;
            }

            throw new NotSupportedException($"Verification Status index {number} is not supported");
        }
    }

    public static class CourseLevel
    {
        public static readonly (string Id, string Title) Elementary = (
            Id: "1",
            Title: "Elementary"
        );

        public static readonly (string Id, string Title) Intermediate = (
            Id: "2",
            Title: "Intermediate"
        );

        public static readonly (string Id, string Title) Advanced = (
            Id: "4",
            Title: "Advanced"
        );

        public static List<(string Id, string Title)> GetCourseLevels()
        {
            return new()
            {
                Elementary, Intermediate, Advanced
            };
        }

        public static string GetTitleByNumber(string number)
        {
            if (Elementary.Id.Equals(number))
            {
                return Elementary.Title;
            }

            if (Intermediate.Id.Equals(number))
            {
                return Intermediate.Title;
            }

            if (Advanced.Id.Equals(number))
            {
                return Advanced.Title;
            }

            throw new NotSupportedException($"Course Level index {number} is not supported");
        }
    }

    public static class Pages
    {
        public const string Homepage = "/";
        public const string Courses = "/course";
        public const string Articles = "/article";
        public const string Contribute = "/contribute";
        public const string AboutUs = "/about-us";
        public const string Certificate = "/certificate";
        public const string LegalPolicy = "/legal-policy";
        public const string Verification = "/verification";
        public const string Network = "/network";
        public const string PublicProfile = "/public-profile";
    }

    public static class ArticleCategoriesFolders
    {
        public const string RootId = "73e448de-c66a-40a4-8c79-e37ed16957fb";
        public const string Governance = "ce0a77bd-e2a9-412c-9cea-45cdbe04c08d";
        public const string InvestmentStewardship = "c5860693-24a6-4913-a888-4ff6aea8a117";
        public const string StewardLeadership = "00c73b44-cd54-4fd8-b094-dd4a0d086fd7"; 
        public const string Sustainability = "5221ebdc-6466-464b-8c89-9c00ea92e6f0";
        public const string Inequality = "78bfe1ba-b123-477e-a68e-bafbfdc6d0cc";
        public const string ClimateChange = "3983df1f-b83e-4216-83a2-ec824ec8c30f";
        

        public static readonly Dictionary<string, string> CategoriesToFoldersMap = new()
        {

            {"fc07689f-0b22-49bf-91d6-d95e92e636bd", Governance},
            {"b3de0996-0574-4472-a5d5-ce8a08ba6395", InvestmentStewardship},
            {"bb17d0a7-9031-46b2-b29a-fac310ec1862", StewardLeadership}, 
            {"f70fd5ca-5349-475a-9c9e-56398d7ecada", Sustainability},
            {"1fb7f553-976d-48d4-959b-444a3d0f3797", Inequality},
            {"f87f2731-c563-4d36-b9d3-ad58dce10de6", ClimateChange}
        };
    }
    
    public static class CoursesCategoriesFolders
    {
        public const string RootId = "ed0e7e9f-e6f8-4939-96bc-1b3489f096ec";
        public const string Environment = "8f96c3e8-6e3c-4a2d-be2a-609bccf23770";
        public const string Social = "5936bf23-9c58-42ff-b277-6ebf1bd61ddc";
        public const string Governance = "6e5b7852-23f0-4499-baa1-7cee2632bea0";
        public const string InvestmentStewardship = "c4c05aff-f638-4154-8e3d-61b3248ab912";
        public const string StewardLeadership = "68d855dd-9660-4de6-a155-75b417ff7aa9";
    }

    public static class ImagesLibrary
    {
        public const string ProfileImages = "fa1ebd56-5255-4814-9f92-fa3bc0fe1fd0";
        public const string Categories = "73e448de-c66a-40a4-8c79-e37ed16957fb";
        public const string DefaultFolder = "042a50be-7ca8-066c-2dad-81e169d78502";
    }

    public static class VideosLibrary
    {
        public const string DefaultFolder = "f9ee6665-0e76-6942-fe3e-f99533a87a3d";
    }

    public static class DocumentsLibrary
    {
        public const string DefaultFolder = "ca8f280a-ee7e-8e4e-d03a-86a47ff6e61a";
        public const string PresentationFolder = "8afbaf24-533f-42e0-9697-50d2b56f8141";
        public const string AudioFolder = "ea8e53a9-8693-4203-b28f-4b248c9136cf";
        public const string PdfFolder = "fe973626-5617-406e-bd17-fcb084655713";
    }

    public static class FileExtension
    {
        public static readonly Dictionary<string, string> ImageExtension = new()
        {
            {".apng", "image/apng"},
            {".gif", "image/gif"},
            {".ico", "image/x-icon"},
            {".cur", "application/octet-stream"},
            {".jpg", "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".jfif", "image/jpeg"},
            {".pjpeg", "image/jpeg"},
            {".pjp", "image/jpeg"},
            {".png", "image/png"},
            {".svg", "image/svg+xml"},
            {".bmp", "image/bmp"}
        };

        public static readonly Dictionary<string, string> VideoExtension = new()
        {
            {".mpg", "video/mpeg4-generic"},
            {".mpeg", "video/mp2t"},
            // { ".avi", "video/x-msvideo" },
            // { ".wmv", "video/x-ms-wmv" },
            {".mov", "video/quicktime"},
            // { ".rm", "application/vnd.rn-realmedia" },
            // { ".swf", "application/x-shockwave-flash" },
            // { ".flv", "video/x-flv" },
            {".webm", "video/webm"},
            {".ogv", "video/ogg"},
            {".mp4", "video/mp4"},
            {".ogg", "video/ogg"}
        };

        public static readonly Dictionary<string, string> AudioExtension = new()
        {
            {".aac", "audio/aac"},
            {".midi", "audio/x-midi"},
            {".mp3", "audio/mpeg"},
            {".ogg", "audio/ogg"},
            {".opus", "audio/opus"},
            {".wav", "audio/wav"},
            {".weba", "audio/webm"},
            {".3gp", "audio/3gpp"}
        };

        public static readonly Dictionary<string, string> PresentationExtension = new()
        {
            {".odp", "application/vnd.oasis.opendocument.presentation"},
            {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
            {".ppt", ".application/vnd.ms-powerpoint"}
        };
        
        public static readonly Dictionary<string, string> PdfExtension = new()
        {
            { ".pdf", "application/pdf" },
        };

        public static readonly Dictionary<string, string> CvExtension = new()
        {
            { ".pdf", "application/pdf" },
            { ".doc", "application/msword" },
            { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" }
        };
    };

    public static class FileSizeLimits
    {
        public const long ImageMaxSize = 20971520; // = 20 * 1024 * 1024 = 20 MB
        public const long VideoMaxSize = 4294967296; // = 4 * 1024 * 1024 * 1024 = 4GB
        public const long AudioMaxSize = 1073741824; // = 1 * 1024 * 1024 * 1024 = 1GB
        public const long DefaultMaxSize = 1073741824; // = 1 * 1024 * 1024 * 1024 = 1GB
        public const long PresentationMaxSize = 52428800; // = 50 * 1024 * 1024 = 50 MB
        public const long PdfMaxSize = 52428800; // = 50 * 1024 * 1024 = 50 MB
        public const long CvMaxSize = 10485760; // = 10 * 1024 * 1024 = 10 MB
    }

    /// <summary>
    /// This dictionary stores key-value according to Sitefinity's signup error callback
    /// Possible error code from Sitefinity:
    /// - DuplicateEmail
    /// - DuplicateUserName
    /// - InvalidAnswer
    /// - InvalidEmail
    /// - InvalidPassword
    /// - InvalidProviderUserKey
    /// - InvalidQuestion
    /// - InvalidUserName
    /// - ProviderError
    /// - Success
    /// - UserRejected
    /// </summary>
    public static readonly Dictionary<string, string> RegistrationErrorMessage = new()
    {
        { "DuplicateUserName", "Account with this email already registered. <a class='text-decoration-none' href='/login'>Login here instead.</a>" },
        { "DuplicateEmail", "Account with this email already registered. <a class='text-decoration-none' href='/login'>Login here instead.</a>" },
        { "Default", "Unknown error. Please try again." }
    };
    

    // Mail Addresses constants
    public static class MailAddress
    {
        public const string Hello = "hello@stewardshipcommons.com";
        public const string Admin = "admin@stewardshipcommons.com";
        public static readonly string[] CCs =
        {
            "shaggy@Stewardshipasia.com.sg",
            "hansiong@Stewardshipasia.com.sg",
            // "thehien.nguyen@zuhlke.com",
            // "vladimir.radzivil@zuhlke.com",
            // "mike.do@zuhlke.com",
            // "nguyenbaotran.doan@zuhlke.com"
        };
    }
}

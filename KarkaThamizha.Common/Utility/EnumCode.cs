using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KarkaThamizha.Common.Utility
{
    public class EnumCode
    {
        public enum Projects
        { KarkaThamizha = 1 }
        public enum ArticleType
        {
            Foreword = 1,	        //	அணிந்துரைகள்
            Literature = 2,         //	இலக்கியம்
            Speches = 3,            //	உரைகள்            
            AuthorProfile = 4,      //	எழுத்தாளர் சுயவிபரம்
            AuthorSeries = 5, 	    //	எழுத்தாளர்கள் தொடர்கள்
            BeyondWriting = 6,      //  எழுத்துக்கு அப்பால் 
            MyFavoriteBooks = 7,    //  எனக்குப் பிடித்த நூல்கள்
            Articles = 8,           //	கட்டுரைகள்
            DiwaliMalar = 9,       //	தீபாவளி மலர்
            StarAuthor = 10,        //  நம்பிக்கை எழுத்தாளர்
            WhyAmIWriting = 11,     //  நான் ஏன் எழுதுகிறேன்?
            Recommend = 12,         //	புத்தக பரிந்துரை
            Interviews = 13,        //	பேட்டிகள், நேர்காணல்கள்
            PongalMalar = 14,       //	பொங்கல் மலர்
            Evaluation = 15,        //	மதிப்பீடுகள்
        }
        public enum EventsType
        {
            TributeShow = 1,                        //அஞ்சலி நிகழ்ச்சி
            DocumentaryRelease = 2,                 //ஆவணப்படம் வெளியீடு 
            LiteratureFestival = 3,                 //இலக்கிய விழா            
            AudioBooks = 4,                         //ஒலிப்புத்தகம் வெளியீட்டு விழா 
            CondemnationText = 5,                   //கண்டன உரை
            Seminar = 6,                            //கருத்தரங்கம்
            Discussion = 7,                         //கலந்துரையாடல்
            SpecialOffer = 8,                       //சிறப்புத் தள்ளுபடி
            SilverEditionReleaseCeremony = 9,       //செம்பதிப்பு வெளியீட்டு விழா
            Discourse = 10,                         //சொற்பொழிவு
            Remembrance = 11,                       //நினைவேந்தல்
            BookLaunch = 12,                        //நூல் அறிமுக விழா
            BookReviewMeeting = 13,                 //நூல் திறனாய்வுக் கூட்டம்
            BookDiscussion = 14,                    //நூல் விவாதம்
            BookRelease = 15,                       //நூல் வெளியீட்டு விழா
            Debate = 16,                            //பட்டிமன்றம்
            AppreciationCeremony = 17,              //பாராட்டு விழா
            BookFair = 18,                          //புத்தகக் கண்காட்சி
            BookPreRelease = 19,                    //முன் வெளியீட்டுத் திட்டம்
            AwardCeremony = 20,                     //விருது வழங்கும் விழா
            Others = 21                             //மற்றவை
        }

        public enum Pages
        {
            [Description("Books Review - All")]
            BooksReviewAll = 1,

            [Description("Magazine Review")]
            MagazineReview = 2,

            [Description("Special Magazine Review")]
            SpecialMagazineReview = 3,

            [Description("Books Review By Author")]
            BooksReviewByAuthor = 4,

            [Description("Books Review By User")]
            BooksReviewByUser = 5,

            [Description("Books Review By Magazine")]
            BooksReviewByMagazine = 6,

            [Description("Author Page")]
            AuthorPage = 7,

            [Description("Author Series")]
            AuthorSeries = 8,

            [Description("Author Interview")]
            AuthorInterview = 9,

            [Description("Author Essays")]
            AuthorEssays = 10,

            [Description("Book PreRelease")]
            BookPreRelease = 11,

            [Description("Book Fair")]
            BookFair = 12,

            [Description("Book Release")]
            BookRelease = 13,

            [Description("Award")]
            Award = 14,

            [Description("ContactUs")]
            ContactUs = 15,

            [Description("Books Special Offer")]
            BooksSpecialOffer = 16,
        }
        public class EnumDisplayNameAttribute : Attribute
        {
            private string _displayName;
            public string DisplayName
            {
                get { return _displayName; }
                set { _displayName = value; }
            }
        }
        public enum ContentView
        {
            All = 0,
            [EnumDisplayName(DisplayName = "Unmatched with Books")]
            UnmatchedWithBooks = 1,
            [EnumDisplayName(DisplayName = "Matched with Books")]
            MatchedWithBooks = 2,
        }
        public enum ShowInterview
        {
            Books,
            Literature,
        }
        public enum ArticleStatus
        {
            Active = 'A',
            InActive = 'I',
            Objection = 'O',
            Pending = 'P',
            Reject = 'R',
        }
        public enum UserType
        {
            [EnumDisplayName(DisplayName = "ஆசிரியர்")]
            Author = 1,

            [EnumDisplayName(DisplayName = "உரையாசிரியர்")]
            TextWriter = 2,

            [EnumDisplayName(DisplayName = "தொகுப்பாசிரியர்")]
            Collector = 3,

            [EnumDisplayName(DisplayName = "பதிப்பகத்தார்")]
            Publisher = 4,

            [EnumDisplayName(DisplayName = "பதிப்பாசிரியர்")]
            Editor = 5,

            [EnumDisplayName(DisplayName = "பயனர்")]
            User = 6,

            [EnumDisplayName(DisplayName = "மொழிபெயர்ப்பாளர்")]
            Translator = 7,
        }
        public enum LoginResult
        {
            WrongUsername, WrongPassword, Success, LockedOut
        }
        public enum TaskStatus
        {
            Created = 1,
            Started = 2,
            Pending = 3,
            Completed = 4
        }
        public enum Priority
        { Low = 1, Medium = 2, High = 3 }
        public enum BloggerType
        {
            Blogger = 'B',
            Wordpress = 'W'
        }

        public enum Category
        {
            அரசியல் = 3,
            இதழ்தொகுப்பு = 7,
            இயற்கை = 8,
            இலக்கியம் = 10,
            கட்டுரைகள் = 13,
            கலை_திரைப்படம் = 15,
            கவிதைகள் = 16,
            சிறார்இலக்கியம் = 18,            
            சங்கஇலக்கியம் = 21,
            சுயசரிதைகள் = 24,
            தன்னம்பிக்கை = 25,
            சொற்பொழிவுகள் = 26,
            நேர்காணல் = 31,
            பயணம் = 33,
            புதினம் = 36,
            மதம் = 39,
            மருத்துவம் = 40,
            நாட்டுப்புறவியல் = 42,
            வேளாண்மை = 47,
            நாடகம் = 125,
            மொழிபெயர்ப்புகவிதைகள் = 218,
            சிறுவர்கதைகள்_நாவல்கள் = 235,
            குறுநாவல் = 351,
            சரித்திர_வரலாற்று_நாவல் = 353,
            சிறுகதைகள் = 354,
            மொழிபெயர்ப்புநாவல் = 360,
            மொழிபெயர்ப்புசிறுகதை = 360            
        }
    }
}

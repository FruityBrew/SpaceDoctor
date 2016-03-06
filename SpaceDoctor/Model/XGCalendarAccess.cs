using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SpaceDoctor.Model
{
    public static class XGCalendarAccess
    {

        #region fields

        readonly static UserCredential _credential;
        readonly static CalendarService _calService;

        #endregion

        #region ctors
        static XGCalendarAccess()
        {
            _credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = "1042911101976-hacnki3op1itrlogb9q2h7ttp2di8tp4.apps.googleusercontent.com",
                    ClientSecret = "RCUEZbs6-71Au7PyONONl8FD",
                },
                new[] { CalendarService.Scope.Calendar },
                "koalse@gmail.ru",
                CancellationToken.None).Result;

            _calService = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = _credential,
                ApplicationName = "fruitybrewproject",
            });
        }
        #endregion


        #region methods
        /// <summary>
        /// Создает и добавляет новое событие в календарь
        /// </summary>
        /// <param name="title"></param>
        /// <param name="location"></param>
        /// <param name="description"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public static bool CreateNewEvent(String googleCalendarAdress, DateTime startdate, DateTime enddate, string title, bool remind, string description = "", string location = "")
        {
            if (startdate == null || enddate == null)
                throw new ArgumentNullException("Аргументы startdate и enddate не могут быть null");
            if (startdate < DateTime.Today || enddate < DateTime.Today || enddate < startdate)
                throw new ArgumentOutOfRangeException("Даты события не могут быть меньше сегодняшней. Дата окончания события не может быть меньше даты начала события");
            if (String.IsNullOrEmpty(title))
                throw new ArgumentException("Аргумент title не может быть пустым или null");

            Event eventdata = new Event();
            eventdata.Summary = title;
            eventdata.Location = location;
            eventdata.Description = description;

            EventDateTime start = new EventDateTime();
            start.DateTime = startdate;
            eventdata.Start = start;
            EventDateTime end = new EventDateTime();
            end.DateTime = enddate;
            eventdata.End = end;

            eventdata.Transparency = "transparent";

            if(remind)
            {

                EventReminder rem = new EventReminder();
                rem.Method = "popup";
                rem.Minutes = 15;
                Event.RemindersData rd = new Event.RemindersData();
                rd.UseDefault = false;
                IList<EventReminder> list = new List<EventReminder>();
                list.Add(rem);
                rd.Overrides = list;
                eventdata.Reminders = rd;
            }


            var insertevent = _calService.Events.Insert(eventdata, googleCalendarAdress);
            Event createdevent = insertevent.Execute();

            return true;
        }
        #endregion

    }
}

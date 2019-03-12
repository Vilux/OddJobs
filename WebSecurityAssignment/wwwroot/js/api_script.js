$.getJSON('http://localhost:49503/api/jobfeedapi',
           function (data) {
               items = JSON.stringify(data);
               
               title = items.title;
               description = items.description;
               date = items.dateNeeded;
               expires = items.dateExpired;
               
           })
$(document).ready(function () {
    $.getJSON('http://oddjobs.azurewebsites.net/api/jobfeedapi',
        function (data) {
            var items = data;
            console.log(items);
            var ul = document.createElement('ul');

            for (i = 0; i < items.length; i++) {
                let li = document.createElement('li');
                let br1 = document.createElement('br');
                let br2 = document.createElement('br');
                let br3 = document.createElement('br');
                let br4 = document.createElement('br');

                let title = document.createTextNode("Title: " + items[i].title);
                let description = document.createTextNode("Description: " + items[i].description);
                let address = document.createTextNode("Address: " + items[i].address);
                let dateNeeded = document.createTextNode("Date Needed: " + items[i].dateNeeded);
                let dateExpired = document.createTextNode("Date Expired: " + items[i].dateExpired);

                li.appendChild(title);
                li.appendChild(br1);
                li.appendChild(description);
                li.appendChild(br2);
                li.appendChild(address);
                li.appendChild(br3);
                li.appendChild(dateNeeded);
                li.appendChild(br4);
                li.appendChild(dateExpired);

                ul.appendChild(li);
            }
            var element = document.getElementById("jobs_api");
            element.appendChild(ul);
           })
        })
function selectCurrent(id) {
    
    let items = document.getElementsByTagName("tr");

    for (let i = 0; i < items.length; i++) {
        if (items[i].id == id) {
            items[i].classList.add('selected_job');
        } else {
            items[i].classList.remove('selected_job');
        }        
    }

}

$(document).ready(function () {

    var array = document.getElementsByTagName("tr");
    var id = array[1].id
    selectCurrent(id)

    var jobID = 0;

    $("tr").click(function () {
        
        jobID = this.id;

        selectCurrent(jobID)

        title = document.getElementById(this.id).getElementsByClassName("job_title")[0].innerHTML;
        title = title.trim();

        description = document.getElementById(this.id).getElementsByClassName("job_description")[0].innerHTML;
        description = description.trim();

        dateNeeded = document.getElementById(this.id).getElementsByClassName("job_dateNeeded")[0].innerHTML;
        dateNeeded = dateNeeded.trim();

        dateExpired = document.getElementById(this.id).getElementsByClassName("job_dateExpired")[0].innerHTML;
        dateExpired = dateExpired.trim();

        address = document.getElementById(this.id).getElementsByClassName("job_address")[0].innerHTML;
        address = address.trim();

        employer = document.getElementById(this.id).getElementsByClassName("job_employer")[0].innerHTML;
        employer = employer.trim();

        amount = document.getElementById(this.id).getElementsByClassName("job_amount")[0].innerHTML;
        amount = amount.trim();


        var details_title = document.getElementById("details_title");
        details_title.innerHTML = "";
        var title_node = document.createTextNode(title);
        details_title.appendChild(title_node);

        var details_description = document.getElementById("details_description");
        details_description.innerHTML = "";
        var description_node = document.createTextNode(description);
        details_description.appendChild(description_node);

        var details_amount = document.getElementById("details_amount");
        details_amount.innerHTML = "";

        var amount_node = document.createTextNode("Pay: " + amount);
        details_amount.appendChild(amount_node);

        var details_dateNeeded = document.getElementById("details_dateNeeded");
        details_dateNeeded.innerHTML = "";
        var dateNeeded_node = document.createTextNode("Available: " + dateNeeded);
        details_dateNeeded.appendChild(dateNeeded_node);

        var details_employer = document.getElementById("details_employer");
        details_employer.innerHTML = "";
        var employer_node = document.createTextNode("Employer: " + employer);
        details_employer.appendChild(employer_node);

        var details_dateExpired = document.getElementById("details_dateExpired");
        details_dateExpired.innerHTML = "";
        var dateExpired_node = document.createTextNode("Expires: " + dateExpired);
        details_dateExpired.appendChild(dateExpired_node);

        var details_address = document.getElementById("details_address");
        details_address.innerHTML = "";
        var address_node = document.createTextNode(address);
        details_address.appendChild(address_node);

  

        $.getJSON('https://maps.googleapis.com/maps/api/geocode/json?address=' + address + '&key=AIzaSyD8owFdPbt2-OK8FLSbhSfZe7qry90ptOA',
            function (data) {

                console.log(JSON.stringify(data));
                longitude = parseFloat(JSON.stringify(data.results[0].geometry.location.lng));
                latitude = parseFloat(JSON.stringify(data.results[0].geometry.location.lat));

                latLng = { lat: latitude, lng: longitude };

                var map = new google.maps.Map(document.getElementById('map'), {
                    center: latLng,
                    zoom: 16
                });

                var marker = new google.maps.Marker({
                    position: latLng,
                    map: map,
                    title: 'Job Location'
                });
            });
    });
    $(".apply_button").click(function () {
        window.location.href = '/Applications/Create/' + jobID;
    });

});
﻿$(document).ready(function () {

    $("tr").click(function () {
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
        var amount_node = document.createTextNode(amount);
        details_amount.appendChild(amount_node);

        var details_dateNeeded = document.getElementById("details_dateNeeded");
        details_dateNeeded.innerHTML = "";
        var dateNeeded_node = document.createTextNode(dateNeeded);
        details_dateNeeded.appendChild(dateNeeded_node);

        var details_employer = document.getElementById("details_employer");
        details_employer.innerHTML = "";
        var employer_node = document.createTextNode(employer);
        details_employer.appendChild(employer_node);

        var details_dateExpired = document.getElementById("details_dateExpired");
        details_dateExpired.innerHTML = "";
        var dateExpired_node = document.createTextNode(dateExpired);
        details_dateExpired.appendChild(dateExpired_node);

        var details_address = document.getElementById("details_address");
        details_address.innerHTML = "";
        var address_node = document.createTextNode(address);
        details_address.appendChild(address_node);
    });
});
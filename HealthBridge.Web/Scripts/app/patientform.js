$(document).ready(function () {
    console.log($("#PatientId").val());
    if ($("#PatientId").val() !== "0") {
        $.get("/api/Patients/getindividualpatient/" + $("#PatientId").val(), function (data) {
            console.log(data);
            $("#FirstName").val(data.FirstName);
            $("#LastName").val(data.LastName);
            $("#IdNumber").val(data.IdNumber);
        });
    }
});

function savePatient() {
    var formData = $("#patientForm").serialize();
    if (!$("#patientForm").valid()) {
        return false;
    }
    $.post("/api/Patients/savepatient/",
        {
            FirstName: $("#FirstName").val(),
            LastName: $("#LastName").val(),
            IdNumber: $("#IdNumber").val()
        })
        .done(function (data) {
            bootbox.alert({
                message: data,
                size: 'small',
                centerVertical: true,
                callback: function () {
                    location.href = 'https://localhost:44305/Patient/PatientList';
                }
            });
            $("#FirstName").val("");
            $("#LastName").val("");
            $("#IdNumber").val("");
        });
}

function updatePatient() {
    var formData = $("#patientForm").serialize();
    if (!$("#patientForm").valid()) {
        return false;
    }
    $.ajax({
        url: '/api/Patients/updatepatient/',
        type: 'PUT',
        data: {
            PatientId: $("#PatientId").val(),
            FirstName: $("#FirstName").val(),
            LastName: $("#LastName").val(),
            IdNumber: $("#IdNumber").val()
        },
        success: function (data) {
            bootbox.alert({
                message: data,
                size: 'small',
                centerVertical: true,
                callback: function () {
                    location.href = 'https://localhost:44305/Patient/PatientList';
                }
            });
            $("#FirstName").val("");
            $("#LastName").val("");
            $("#IdNumber").val("");
        }
    });
}
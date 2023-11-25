Office.onReady(function (info) {
    // Office.onReady is an event that is called when the Office.js library is loaded
    if (info.host === Office.HostType.Outlook) {
        // Get the currently signed-in user's email address
        var userEmail = Office.context.mailbox.userProfile.emailAddress;

        // Call the function to assign the role based on the email address
        var userRole = assignRoleByEmail(userEmail);

        // Call a function to initialize classifications based on the user's information
        initializeClassifications(userEmail, userRole);
    }
});

function assignRoleByEmail(email) {
    if (email === "NSAProgramTester1@outlook.com") {
        return "Head of Department";
    } else if (email === "NSAProgramTester2@outlook.com") {
        return "Employee";
    } else if (email === "NSAProgramTester3@outlook.com") {
        return "Intern";
    } else {
        // Default role if the email doesn't match any of the specified addresses
        return "Unknown";
    }
}


function initializeClassifications(email, role) {
    // Implement logic to set up classifications based on email and role
    var classifications = [];

    if (role === "Head of Department") {
        classifications = ["TOP SECRET", "SECRET", "CONFIDENTIAL", "UNCLASSIFIED", "HCS", "COMINT", "-GAMMA", "-ECI", "TALENT KEYHOLE", "TS", "S", "C", "U", "HCS", "SI", "-G", "-ECI XXX", "TK"];
    } else if (role === "Employee") {
        classifications = ["SECRET", "CONFIDENTIAL", "UNCLASSIFIED", "HCS", "COMINT", "TALENT KEYHOLE", "C", "U", "HCS", "SI", "TK"];
    } else if (role === "Intern") {
        classifications = ["CONFIDENTIAL", "UNCLASSIFIED", "HCS", "COMINT", "C", "U", "HCS", "SI"];
    }

    // Now you can use the 'classifications' array as needed in your add-in
    // For example, you can use it to control access to certain features or data
}


//THIS WAS ADDED USING CHATGPT, IT MAY NOT WORK ACCORDINGLY
function sendEmailBodyToCSharp() {
    // Get the current item (email) in Outlook
    var item = Office.context.mailbox.item;

    // Get the body of the email
    var body = item.body.getAsync({ "asyncContext": "This is the body of the email." }, function (result) {
        if (result.status === "succeeded") {
            // Send the body to the C# endpoint
            sendToCSharp(result.value);
        } else {
            console.error(result.error.message);
        }
    });
}

function sendToCSharp(body) {
    // You can use XMLHttpRequest or any other method to send the data to your C# endpoint
    var xhr = new XMLHttpRequest();
    var url = 'http://your-csharp-endpoint';

    xhr.open('POST', url, true);
    xhr.setRequestHeader('Content-Type', 'application/json');

    // Handle the response from the C# endpoint
    xhr.onload = function () {
        if (xhr.status === 200) {
            console.log('Data sent successfully to C# endpoint.');
        } else {
            console.error('Error sending data to C# endpoint. Status: ' + xhr.status);
        }
    };

    // Convert the body to JSON and send the request
    xhr.send(JSON.stringify({ body: body }));
}

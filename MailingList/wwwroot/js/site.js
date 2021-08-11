const uri = 'api/MailingContacts';


function addContact() {
    const firstNameTextbox = document.getElementById('first-name');
    const lastNameTextbox = document.getElementById('last-name');
    const emailTextbox = document.getElementById('email');

    const contact = {
        FirstName: firstNameTextbox.value.trim(),
        LastName: lastNameTextbox.value.trim(),
        Email: emailTextbox.value.trim(),
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(contact)
    })
        .then(response => response.json())
        .then(() => {
            alert("Contact added");
        })
        .catch(error => console.error('Unable to add contact.', error));
}

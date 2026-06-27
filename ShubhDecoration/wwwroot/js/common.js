function validateMobileNumber() {
    var value = $('#Phone').val().trim();
    if (value != null && value != undefined && value !== "") {
        let firstChar = value[0];
        if (!isNaN(firstChar)) {
            if (firstChar == 6 || firstChar > 6) {
                const regex = /^[6-9]\d{9}$/;
                if (regex.test(value)) {
                    $('.error-message').text('');
                    return true;
                }
                else {
                    $('.error-message').text('Please enter a valid 10-digit mobile number starting with 6, 7, 8, or 9!');
                    return false;
                }
            }
            else {
                $('.error-message').text("Error: Plese enter the mobile number first digit 6 or is greater than 6!");
                return false;
            }
        }
        else {
            $('#Phone').val('')
            $('.error-message').text("Error: Plese enter the mobile number first digit is number!");
            return false;
        }
    }
}

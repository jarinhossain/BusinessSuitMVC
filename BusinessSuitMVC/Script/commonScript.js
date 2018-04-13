function maxLengthCheck(object) {

    if (object.value.length > object.maxLength)
        object.value = object.value.slice(0, object.maxLength)
    // if((inputValue < 1 || inputValue > 5) && inputValue < 10)
    // object.value = ""
    if (object.value < object.min || object.value > object.max)
        object.value = ""
}
function maxLengthCheckOnly(object) {

    if (object.value.length > object.maxLength)
        object.value = object.value.slice(0, object.maxLength)
}
function forceNumeric() {
    var $input = $(this);
    //console.log($input);

    var flag;
    var variable;
    var regx = /[^\d]+/g;

    $input.val($input.val().replace(regx, ''));

}
$('body').on('propertychange input', 'input[type="number"]', forceNumeric);
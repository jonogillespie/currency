$(document).ready(() => {
  loadCurrencies();
});

$("#calculator-form").submit((event) => {

  event.preventDefault();
  
  convert();

});

const loadCurrencies = () => {
  const settings = {
    "url": "http://localhost:8080/v1/currencies/abbreviations",
    "method": "GET",
    "timeout": 0,
  };

  $.ajax(settings).done((response) => {

    let html = '';
    response.forEach(element => {
      html += `<option>${element}</option>`
    });

    const dropdown = $('#currency-select');
    dropdown.append(html);
  });
}

const convert = () => {

  const data = $('#calculator-form')
    .serializeArray()
    .reduce(function (obj, item) {
      obj[item.name] = item.value;
      return obj;
    }, {});

  const settings = {
    "url": `http://localhost:8080/v1/currencies/${data.currency}/conversions/${data.amount}`,
    "method": "GET",
    "timeout": 0,
  };

  $.ajax(settings).done((response) => {
    $('#display-currency').empty().append(`<b>${response} BTC</b>`)
  });
}

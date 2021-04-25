$(document).ready(() => {
  loadData();
  
  setInterval(function(){
    loadData() 
  }, 3000);
});

const loadData = () => {
  const settings = {
    "url": "http://localhost:8080/v1/currencies",
    "method": "GET",
    "timeout": 0,
  };

  $.ajax(settings).done((response) => {

    let html = '';
    response.data.forEach(element => {
      html += buildRow(element);
    });

    const currencyTableRow = $('#currency-table tr');
    currencyTableRow.not(':first').not(':last').remove()
    currencyTableRow.first().after(html);
  });
}

const buildRow = (element) => {
  return '<tr>' +
    '         <td>' + element.abbreviation + '</td>' +
    '         <td>' + element["15m"] + '</td>' +
    '         <td>' + element.last + '</td>' +
    '         <td>' + element.buy + '</td>' +
    '         <td>' + element.sell + '</td>' +
    '         <td>' + element.symbol + '</td>' +
    '      </tr>';
}

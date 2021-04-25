$(document).ready(() => {

  loadData()
  
  setInterval(function(){

    loadData()

  }, 3000)

})

const loadData = () => {

  const settings = {
    url: `${baseUrl}/v1/currencies`,
    method: "GET",
    timeout: 0
  }

  $.ajax(settings).done((response) => {

    let html = buildAllRowsHtml(response)

    addRowsToTable(html)

    hideErrorMessage()

  }).fail(() => {

    showErrorMessage()
  })
}

const buildAllRowsHtml = (response) => {

  let html = ''

  response.data.forEach(element => {
    html += buildRow(element)
  })

  return html
}

const buildRow = (element) =>
    '<tr>' +
    '         <td>' + element.abbreviation + '</td>' +
    '         <td>' + element["15m"] + '</td>' +
    '         <td>' + element.last + '</td>' +
    '         <td>' + element.buy + '</td>' +
    '         <td>' + element.sell + '</td>' +
    '         <td>' + element.symbol + '</td>' +
    '      </tr>'

const addRowsToTable = (html) => {

  const currencyTableRow = $('#currency-table tr')

  removeCurrentTableRows(currencyTableRow)

  currencyTableRow.first().after(html)
}

const removeCurrentTableRows = (currencyTableRow) =>
    currencyTableRow.not(':first').not(':last').remove()

const hideErrorMessage = () =>
    $('#error-message').hide()

const showErrorMessage = () =>
    $('#error-message').show()

const baseUrl = 'http://localhost:9080'

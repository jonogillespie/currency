
$(document).ready(() => {
  loadCurrencies()
})

$("#calculator-form").submit((event) => {
  event.preventDefault()
  performConversion()
})

const loadCurrencies = () => {
  const settings = {
    url: `${baseUrl}/v1/currencies/abbreviations`,
    method: "GET",
    timeout: 0
  }

  $.ajax(settings).done((response) => {
    populateSelectBox(response)
  }).fail(() => {
    showErrorMessage('An error occurred fetching the currencies. Please try again later.')
  })
}

const performConversion = () => {

  const data = getFormData()

  const settings = {
    url: `${baseUrl}/v1/currencies/${data.currency}/conversions/${data.amount}`,
    method: "GET",
    timeout: 0
  }

  $.ajax(settings).done((response) => {

    displayAmount(response)

    hideErrorMessage()

  }).fail(() => {

    showErrorMessage('An error occurred performing your conversion. ' +
        'Please try again later')

  })
}

const buildSelectBoxOptionsHtml = (response) => {

  let html = ''

  response.forEach(element => {
    html += `<option>${element}</option>`
  })

  return html
}

const appendSelectBoxOptions = (html) => {

  const dropdown = $('#currency-select')

  dropdown.append(html)
}

const populateSelectBox = (response) => {

  let html = buildSelectBoxOptionsHtml(response)

  appendSelectBoxOptions(html)
}

const getFormData = () =>
    $('#calculator-form')
        .serializeArray()
        .reduce(function (obj, item) {
          obj[item.name] = item.value
          return obj
        }, {})

const displayAmount = (response) =>
    $('#display-currency').empty().append(`<b>${response} BTC</b>`)

const hideErrorMessage = () =>
    $('#error-message').hide()

const showErrorMessage = (message) => {

  const errorMessage = $('#error-message')

  errorMessage.text(message)

  errorMessage.show()
}

const baseUrl = 'http://localhost:9080'

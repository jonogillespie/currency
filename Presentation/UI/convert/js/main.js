
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

  $.ajax(settings).done((items) => {
    populateSelectBox(items)
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

  $.ajax(settings).done((amount) => {

    displayAmount(amount)

    hideErrorMessage()

  }).fail(() => {

    showErrorMessage('An error occurred performing your conversion. ' +
        'Please try again later')

  })
}

const buildSelectBoxOptionsHtml = (items) => {

  let html = ''

  items.forEach(element => {
    html += `<option>${element}</option>`
  })

  return html
}

const appendSelectBoxOptions = (html) => {

  const dropdown = $('#currency-select')

  dropdown.append(html)
}

const populateSelectBox = (items) => {

  let html = buildSelectBoxOptionsHtml(items)

  appendSelectBoxOptions(html)
}

const getFormData = () =>
    $('#calculator-form')
        .serializeArray()
        .reduce(function (obj, item) {
          obj[item.name] = item.value
          return obj
        }, {})

const displayAmount = (amount) =>
    $('#display-currency').empty().append(`<b>${amount} BTC</b>`)

const hideErrorMessage = () =>
    $('#error-message').hide()

const showErrorMessage = (message) => {

  const errorMessage = $('#error-message')

  errorMessage.text(message)

  errorMessage.show()
}

const baseUrl = 'http://localhost:9080'

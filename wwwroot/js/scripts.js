
$(document).ajaxSuccess(function () {
  changeNumbersToPersian()
  addPricesVirgols()
});
var typeKon = function (res) {
  console.log(res)
};
async function httpRequest(url, method, success, formData, begin, complete) {
  await $.ajax({
    url,
    method,
    data: formData,
    beforeSend: () => {
      if (begin) {
        begin();
      }
    },
    complete: () => {
      if (complete) {
        complete();
      }
    },
    success: (res) => {
      if (success) {
        success(res);
      }
    },
    error: () => {
    },
    beforeSend: () => {
      if (begin) {
        begin();
      }
    },
  });
}
async function getPartialView(containerId, url) {
  console.log(containerId)
  console.log(url)
  var element = document.getElementById(containerId);
  if (element) {

    openModal('loading-modal')
    await httpRequest(
      url,
      "GET",
      function success(res) {
        changeInnerHtml([[containerId, res]])
      }
    );
    closeModal('loading-modal')
  }
}
function CreateElement(elementName, classList = null, id = null) {
  var element = document.createElement(elementName);
  if (id) {
    element.id = id
  }
  if (classList) {
    classList.forEach(className => {
      element.classList.add(className)
    });
  }
  return element;
}
function enableFormsValidation() {
  $.validator.unobtrusive.parse(document);
}
const toastBox = document.getElementById("toast-box")
function showToast(type, message) {
  let toast = document.createElement('div')
  let messageSpan = document.createElement('span')
  messageSpan.classList.add('c-toast-message')
  messageSpan.innerText = message
  toast.classList.add('c-toast', type)
  toast.appendChild(messageSpan)
  type == 'success' ? toast.innerHTML += '<i class="fa-solid fa-circle-check"></i>' :
    type == 'error' ? toast.innerHTML += '<i class="fa-solid fa-circle-xmark"></i>' :
      type == 'warning' ? toast.innerHTML += '<i class="fa-solid fa-circle-exclamation"></i>' : 0;
  toastBox.appendChild(toast)
  setTimeout(() => {
    toast.remove();
  }, 3000);
}
function filterDropdown(event, dropdownContent) {
  const searchQuery = event.target.value.toLowerCase();
  const lists = document.querySelectorAll("#" + dropdownContent + " li");
  const options = document.querySelectorAll("#" + dropdownContent + " [search-text='true']");
  for (let i = 0; i < lists.length; i++) {
    if (options[i].innerText.toLowerCase().indexOf(searchQuery) > -1) {
      lists[i].style.display = "";
    } else {
      lists[i].style.display = "none";
    }
  }
}
function effectCurrentPageLinkElement() {
  const currentUrl = window.location.href;
  const links = document.getElementsByTagName('a');
  for (let i = 0; i < links.length; i++) {
    if (currentUrl == links[i].href) {
      links[i].classList.add('text-pink-500')
      links[i].classList.add('hover:text-pink-600')
    }
  }
}
function openModal(modalId) {
  let modal = document.getElementById(modalId);
  if (modal) {
    modal.classList.remove("hidden")
    modal.classList.remove("pointer-events-none")
  }
}
function closeModal(modalID) {
  let modal = document.getElementById(modalID);
  if (modal) {
    modal.classList.add("hidden")
    modal.classList.add("pointer-events-none")
    let func = modal.getAttribute("onClose")
    if (func) {
      eval(func);
      modal.removeAttribute("onClose")
    }
  }
}
function enableButtonLoading(buttonText, buttonLoading) {
  let text = document.getElementById(buttonText);
  let loading = document.getElementById(buttonLoading);
  if (text) {
    text.classList.add("hidden")
  }
  if (loading) {
    loading.classList.remove("hidden")
  }
}
function disableButtonLoading(buttonText, buttonLoading) {
  let text = document.getElementById(buttonText);
  let loading = document.getElementById(buttonLoading);
  if (text) {
    text.classList.remove("hidden")
  }
  if (loading) {
    loading.classList.add("hidden")
  }
}
function disableButton(buttonId) {
  const button = document.getElementById(buttonId)
  button.disabled = true;
}
function enableButton(buttonId) {
  const button = document.getElementById(buttonId)
  button.disabled = false;
}
function removeElementFromParent(result, parentId) {
  const parent = document.getElementById(parentId);
  parent.innerHTML = parent.innerHTML - result
}
function addElementToParent(result, parentId) {
  const parent = document.getElementById(parentId);
  if (typeof (result) === "string") {
    parent.innerHTML += result
  }
  else {
    parent.appendChild(result)
  }
}
function resetFormInputs(formId) {
  let form = document.getElementById(formId);
  if (form) {
    let inputs = form.getElementsByTagName("input");
    let textareas = form.getElementsByTagName("textarea");
    for (let i = 0; i < inputs.length; i++) {
      inputs[i].value = "";
    }
    for (let i = 0; i < textareas.length; i++) {
      textareas[i].value = "";
    }

  }
}
function changeInnerHtml(arrays) {
  arrays.forEach((array) => {
    let element = document.getElementById(array[0]);
    if (element) {
      element.innerHTML = array[1];
    }
  });
}
function showDropdown(dropdown, dropdownContent) {
  removeClassFromElement([[dropdownContent, 'hidden']])
  document.addEventListener("click", hideDropdown);
  function hideDropdown(event) {
    if (event) {
      if (!event.target.closest("#" + dropdown)) {
        addClassToElement([[dropdownContent, 'hidden']])
        document.removeEventListener("click", hideDropdown);
      }
    }
  }
}
function hideDropdown(dropdownContent) {
  addClassToElement([[dropdownContent, 'hidden']])
  document.removeEventListener("click", hideDropdown);
}
function addClassToElement(arrays) {
  arrays.forEach((array) => {
    let element = document.getElementById(array[0]);
    if (element) {
      element.classList.add(array[1]);
    }
  });
}
function toggleClassToElement(arrays) {
  arrays.forEach((array) => {
    let element = document.getElementById(array[0]);
    if (element) {
      if (element.classList.contains(array[1])) {
        element.classList.remove(array[1]);
      }
      else {
        element.classList.add(array[1]);
      }
    }
  });
}
function removeClassFromElement(arrays) {
  arrays.forEach((array) => {
    let element = document.getElementById(array[0]);
    if (element) {
      element.classList.remove(array[1]);
    }
  });
}
function deleteElement(elementId) {

  let element = document.getElementById(elementId);
  element.remove()
}
function previewImage(e, inputId, previewId) {
  let fileInput = document.getElementById(inputId)
  var files = e.target.files[0]
  var fileReader = new FileReader();
  fileReader.onload = (function (e) {
    var span = CreateElement("span"
      , classList = ["pip", "relative"])
    var img = CreateElement("img"
      , classList = ["imageThumb"])
    img.src = e.target.result
    span.appendChild(img)

    let closeIcon = CreateElement("i"
      , classList = ["fa", "fa-trash", "absolute", "right-0", "top-0", "opacity-50", "hover:opacity-100", "bg-white", "p-1", "rounded", "cursor-pointer"])
    span.appendChild(closeIcon)

    closeIcon.onclick = async () => {
      span.remove()
      let dataTransfer = new DataTransfer();
      fileInput.files = dataTransfer.files
    }
    let previews = document.getElementById(previewId)
    previews.innerHTML = ""
    previews.append(span)
  });
  fileReader.readAsDataURL(files);
  fileInput.files = files
}
function deletePreview(previewId, inputId) {
  const preview = document.getElementById(previewId);
  preview.classList.add("hidden")
  document.getElementById(inputId).value = "";
}
async function GetImagesFilesDataTransfer(elementId) {
  let elements = document.querySelectorAll("#" + elementId + " img")
  let dataTransfer = new DataTransfer();
  for (let i = 0; i < elements.length; i++) {
    const img = elements[i]
    await fetch(img.src)
      .then(res => res.blob())
      .then(blob => {
        let file = new File([blob], 'photo.png', blob)
        dataTransfer.items.add(file);
      })
  }
  return dataTransfer;
}
async function previewGallery(e, inputId, previewId) {
  let fileInput = document.getElementById(inputId)
  var files = e.target.files
  for (var i = 0; i < files.length; i++) {
    var fileReader = new FileReader();
    fileReader.onload = (function (e) {
      var span = CreateElement("span"
        , classList = ["pip", "relative"])
      var img = CreateElement("img"
        , classList = ["imageThumb"])
      img.src = e.target.result
      span.appendChild(img)

      let closeIcon = CreateElement("i"
        , classList = ["fa", "fa-trash", "absolute", "right-0", "top-0", "opacity-50", "hover:opacity-100", "bg-white", "p-1", "rounded", "cursor-pointer"])
      span.appendChild(closeIcon)

      closeIcon.onclick = async () => {
        span.remove()
        let dataTransfer = await GetImagesFilesDataTransfer(previewId);
        fileInput.files = dataTransfer.files
      }
      let previews = document.getElementById(previewId)
      previews.appendChild(span)
    });
    fileReader.readAsDataURL(files[i]);
  }
  let dataTransfer = await GetImagesFilesDataTransfer(previewId);
  for (let i = 0; i < files.length; i++) {
    dataTransfer.items.add(files[i]);
  }
  fileInput.files = dataTransfer.files
}
function showBackendResponseResult(result) {
  if (result.status >= 400 && result.status < 600) {
    result.responseText ? showToast("error", result.responseText) : showToast("error", "عملیات با خطا مواجه شد.")
  }
  else if (result.status >= 200 && result.status < 300) {
    result.responseText ? showToast("success", result.responseText) : showToast("error", "عملیات با خطا مواجه شد.")
  }
  else {
    result.responseText ? showToast("warning", "خطای نامشخص") : 0
  }
}
function Redirect(result) {
  if (result.redirect) {
    if (result.replace = true) {
      window.location.replace(result.redirect);
    }
    else {
      window.location.href = result.redirect;
    }
  }
}
function increaseInnerElementValue(arrays) {
  arrays.forEach((array) => {
    let element = document.getElementById(array[0]);
    if (element) {
      let currentValue = parseInt(element.innerText)
      if (array[2]) {
        if (currentValue < array[2]) {
          element.innerText = currentValue + array[1]
        }
      }
      else {
        element.innerText = currentValue + array[1]
      }
    }
  });
}
function decreaseInnerElementValue(arrays) {
  arrays.forEach((array) => {
    let element = document.getElementById(array[0]);
    if (element) {
      let currentValue = parseInt(element.innerText)
      if (currentValue > 1) {
        element.innerText = currentValue - array[1]
      }
    }
  });
}
function changeAttribute(arrays) {
  arrays.forEach((array) => {
    let element = document.getElementById(array[0]);
    element.setAttribute(array[1], array[2]);
  });
}
function getInnerText(id) {
  const element = document.getElementById(id);
  return element.innerText;
}
function changeNumbersToPersian() {
  var map = [
    "&#1632;", "&#1633;", "&#1634;", "&#1635;", "&#1636;",
    "&#1637;", "&#1638;", "&#1639;", "&#1640;", "&#1641;"
  ];
  var elements = document.querySelectorAll('[persian-number]');
  for (var i = 0; i < elements.length; i++) {
    elements[i].innerHTML = elements[i].innerHTML.replace(
      /\d(?=[^<>]*(<|$))/g,
      function ($0) {
        return map[$0];
      }
    );
  }
}
function addPricesVirgols() {
  var elements = document.querySelectorAll('[price]');
  for (var i = 0; i < elements.length; i++) {

    let str = elements[i].innerText

    str = str.replace(/,/g, "");

    let temp = ""
    for (let i = 0; i < str.length; i++) {
      if (((str.length - i - 1)) != 0 && (str.length - i - 1) % 3 == 0) {
        temp += str[i] + ","
      }
      else {
        temp += str[i]
      }
    }
    elements[i].innerText = temp
  }
}
function activeItemInList(ulId, itemId, className) {
  const ul = document.getElementById(ulId);
  const items = ul.getElementsByTagName("li");
  for (let i = 0; i < items.length; i++) {
    items[i].classList.remove(className);
  }

  const item = document.getElementById(itemId);
  item.classList.add(className);
}


addPricesVirgols();
changeNumbersToPersian();

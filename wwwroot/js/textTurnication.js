var div = document.getElementById('TurnDiv');
var characterLimit = 300; 

if (div.textContent.length > characterLimit) {
  div.textContent = div.textContent.substring(0, characterLimit) + '...';
}


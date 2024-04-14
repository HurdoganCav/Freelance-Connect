var div = document.getElementById('TurnDivApliedJobs');
var characterLimit = 300; 

if (div.textContent.length > characterLimit) {
  div.textContent = div.textContent.substring(0, characterLimit) + '...';
}

var div = document.getElementById('TurnDivPostedJobs');
var characterLimit = 300; 

if (div.textContent.length > characterLimit) {
  div.textContent = div.textContent.substring(0, characterLimit) + '...';
}
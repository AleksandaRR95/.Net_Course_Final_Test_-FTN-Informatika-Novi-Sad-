var host = "https://localhost:";
var port = "44359/";
var loginEndpoint = "api/authentication/login";
var registerEndpoint = "api/authentication/register";
var paketEndpoint = "api/Paketi/";
var kurirEndpoint = "api/Kuriri/";
var SearchEndpoint = "api/pretraga"
var formAction = "Create";
var editingId;
var jwt_token;


function registerUser() {
	var username = document.getElementById("usernameRegister").value;
	var email = document.getElementById("emailRegister").value;
	var password = document.getElementById("passwordRegister").value;
	var confirmPassword = document.getElementById("confirmPasswordRegister").value;

	if (validateRegisterForm(username, email, password, confirmPassword)) {
		var url = host + port + registerEndpoint;
		var sendData = { "Username": username, "Email": email, "Password": password };
		fetch(url, { method: "POST", headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(sendData) })
			.then((response) => {
				if (response.status === 200) {
					console.log("Successful registration");
					alert("Successful registration");
					showLogin();
				} else {
					console.log("Error occured with code " + response.status);
					console.log(response);
					alert("Desila se greska!");
				}
			})
			.catch(error => console.log(error));
	}
	return false;
}
function validateLoginForm(username, password) {
	if (username.length === 0) {
		alert("Username field can not be empty.");
		return false;
	} else if (password.length === 0) {
		alert("Password field can not be empty.");
		return false;
	}
	return true;
}
function validateRegisterForm(username, email, password, confirmPassword) {
	if (username.length === 0) {
		alert("Username field can not be empty.");
		return false;
	} else if (email.length === 0) {
		alert("Email field can not be empty.");
		return false;
	} else if (password.length === 0) {
		alert("Password field can not be empty.");
		return false;
	} else if (confirmPassword.length === 0) {
		alert("Confirm password field can not be empty.");
		return false;
	} else if (password !== confirmPassword) {
		alert("Password value and confirm password value should match.");
		return false;
	}
	return true;
}
function loginUser() {
	var username = document.getElementById("usernameLogin").value;
	var password = document.getElementById("passwordLogin").value;

	if (validateLoginForm(username, password)) {
		var url = host + port + loginEndpoint;
		var sendData = { "Username": username, "Password": password };
		fetch(url, { method: "POST", headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(sendData) })
			.then((response) => {
				if (response.status === 200) {
					console.log("Successful login");
					alert("Successful login");
					response.json().then(function (data) {
						console.log(data);
						document.getElementById("info").innerHTML = "Prijavljeni korisnik: <i>" + data.username + "<i/>.";
						document.getElementById("logout").style.display = "block";
						document.getElementById("btnLogin").style.display = "none";
						document.getElementById("btnRegister").style.display = "none";
                        document.getElementById("searchDiv").style.display = "block";
                        

						jwt_token = data.token;
						loadPaketi();
                        loadKuriri();
					});
				} else {
					console.log("Error occured with code " + response.status);
					console.log(response);
					alert("Desila se greska!");
				}
			})
			.catch(error => console.log(error));
	}
	return false;
}
function logout() {
	jwt_token = undefined;
	document.getElementById("info").innerHTML = "";
	document.getElementById("data").style.display = "none";
	document.getElementById("formDiv").style.display = "none";
	document.getElementById("loginFormDiv").style.display = "block";
	document.getElementById("registerFormDiv").style.display = "none";
	document.getElementById("logout").style.display = "none";
	document.getElementById("btnLogin").style.display = "initial";
	document.getElementById("btnRegister").style.display = "initial";
	document.getElementById("searchDiv").style.display = "none";

}
function showRegistration() {
	document.getElementById("data").style.display = "block";
	document.getElementById("formDiv").style.display = "none";
	document.getElementById("loginFormDiv").style.display = "none";
	document.getElementById("registerFormDiv").style.display = "block";
	document.getElementById("logout").style.display = "none";
	document.getElementById("searchDiv").style.display = "none";
    document.getElementById("btnLogin").style.display = "none";
    document.getElementById("btnRegister").style.display = "none";


}
function showLogin() {
	document.getElementById("data").style.display = "block";
	document.getElementById("formDiv").style.display = "none";
	document.getElementById("loginFormDiv").style.display = "block";
	document.getElementById("registerFormDiv").style.display = "none";
	document.getElementById("logout").style.display = "none";
	document.getElementById("searchDiv").style.display = "none";
    document.getElementById("btnLogin").style.display = "none";
    document.getElementById("btnRegister").style.display = "none";


}


function createHeader() {
	var thead = document.createElement("thead");
	var row = document.createElement("tr");
	
	row.appendChild(createTableCell("Posilajac"));
	row.appendChild(createTableCell("Primalac"));
    row.appendChild(createTableCell("Tezina(kg)"));
	row.appendChild(createTableCell("Kurir"));
    if(jwt_token){
    row.appendChild(createTableCell("Postarina(din)"));
    row.appendChild(createTableCell("Akcija"));
    
    }
	
	thead.appendChild(row);
	return thead;
}
function createTableCell(text) {
	var cell = document.createElement("td");
	var cellText = document.createTextNode(text);
	cell.appendChild(cellText);
	return cell;
}

function loadPaketi() {
	document.getElementById("data").style.display = "block";
	document.getElementById("loginFormDiv").style.display = "none";
	document.getElementById("registerFormDiv").style.display = "none";
    document.getElementById("formDiv").style.display = "none";
    document.getElementById("btnLogin").style.display = "initial";
    document.getElementById("btnRegister").style.display = "initial";
	// ucitavanje festivala
	var requestUrl = host + port + paketEndpoint;
	console.log("URL zahteva: " + requestUrl);
	var headers = {};
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;			
	}
	console.log(headers);
	fetch(requestUrl, { headers: headers })
		.then((response) => {
			if (response.status === 200) {
				response.json().then(setPaketi);
			} else {
				console.log("Error occured with code " + response.status);
				showError();
			}
		})
		.catch(error => console.log(error));
};

function setPaketi(data) {
	var container = document.getElementById("data");
	container.innerHTML = "";

	console.log(data);

	// ispis naslova
	var div = document.createElement("div");
	var h1 = document.createElement("h1");
	var headingText = document.createTextNode("Paketi");
	h1.appendChild(headingText);
	div.appendChild(h1);

	// ispis tabele
	var table = document.createElement("table");
	table.className = "table table-striped";
	var header = createHeader();
	table.append(header);
	header.style.backgroundColor = "pink";

	var tableBody = document.createElement("tbody");

	for (var i = 0; i < data.length; i++)
	{
        
		// prikazujemo novi red u tabeli
		var row = document.createElement("tr");
		// prikaz podataka
		row.appendChild(createTableCell(data[i].posiljalac));
        row.appendChild(createTableCell(data[i].primalac));
		row.appendChild(createTableCell(data[i].tezina));
		row.appendChild(createTableCell(data[i].kurirIme));
        if(jwt_token){
            row.appendChild(createTableCell(data[i].cenaPostarine));
            document.getElementById("maliInfo").style.display = "none";
	    document.getElementById("loginFormDiv").style.display = "none";
        document.getElementById("formDiv").style.display = "block";
         
            	var stringId = data[i].id.toString();

		var buttonDelete = document.createElement("button");
		buttonDelete.name = stringId;
		buttonDelete.className = "btn btn-danger";
		buttonDelete.addEventListener("click", deletePaket);
		var buttonDeleteText = document.createTextNode("Obrisi");
		buttonDelete.appendChild(buttonDeleteText);
		var buttonDeleteCell = document.createElement("td");
		
		buttonDeleteCell.appendChild(buttonDelete);
		row.appendChild(buttonDeleteCell);
           
  
        }
        tableBody.appendChild(row);	
		
	}

	table.appendChild(tableBody);
	div.appendChild(table);



	// ispis novog sadrzaja
	container.appendChild(div);
};
function deletePaket(){
    var deleteID = this.name;
	// saljemo zahtev 
	var url = host + port + paketEndpoint + deleteID.toString();
	var headers = { 'Content-Type': 'application/json' };
	if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;		// headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
	}
	fetch(url, { method: "DELETE", headers: headers})
		.then((response) => {
			if (response.status === 204) {
				console.log("Successful action");
				refreshTable();
			} else {
				console.log("Error occured with code " + response.status);
				alert("Desila se greska!");
			}
		})
		.catch(error => console.log(error));
};

function loadKuriri(){
	var requestUrl = host + port + kurirEndpoint;
	console.log("URL zahteva: " + requestUrl);
	fetch(requestUrl)
		.then((response) => {
			if (response.status === 200) {
				response.json().then(setKuririInput);
			} else {
				console.log("Error occured with code " + response.status);
				showError();
			}
		})
		.catch(error => console.log(error));

}

function setKuririInput(data){
	var dropdown = document.getElementById("paketKuriri");
	for (var i = 0; i < data.length; i++) {
		var option = document.createElement("option");
		option.value = data[i].id;
		var text = document.createTextNode(data[i].ime);
		option.appendChild(text);
		dropdown.appendChild(option);
	}
};


function submitPaketForm(){

	var posiljalac = document.getElementById("paketPosiljalac").value;
	var primalac = document.getElementById("paketPrimalac").value;
	var tezina = document.getElementById("paketTezina").value;
	var cena = document.getElementById("paketCena").value;
	var kurir = document.getElementById("paketKuriri").value;

	
	var httpAction;
	var sendData;
	var url;
	
if(validatePaketForm()){
	if (formAction === "Create") {
		httpAction = "POST";
		url = host + port + paketEndpoint;
		sendData = {
			"Posiljalac": posiljalac,
			"Primalac": primalac,
			"Tezina" : tezina,
			"CenaPostarine": cena,
			"KurirId" : kurir
		};
	}


	console.log("Objekat za slanje");
	console.log(sendData);
    var headers = { 'Content-Type': 'application/json' };
    if (jwt_token) {
		headers.Authorization = 'Bearer ' + jwt_token;		// headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
	}

	fetch(url, { method: httpAction, headers : headers, body: JSON.stringify(sendData) })
		.then((response) => {
			if (response.status === 200 || response.status === 201) {
				console.log("Successful action");
				formAction = "Create";
				document.getElementById("paketiForm").reset();
				refreshTable();
			} else {
				console.log("Error occured with code " + response.status);
				alert("Error occured!");
			}
		})
		.catch(error => console.log(error));
	return false;
	}
	return false;
};

function validatePaketForm(){
	var posiljalac = document.getElementById("paketPosiljalac").value;
	var primalac = document.getElementById("paketPrimalac").value;
	var tezina = document.getElementById("paketTezina").value;
	var cena = document.getElementById("paketCena").value;

	if(posiljalac.length <= 2 || posiljalac.length > 90){
		alert("Dozvoljen broj karaktera za posiljaoca je izmedju 2 i 90");
		return false;
	}else if(primalac.length <= 2 || primalac.length > 90){
        alert("Dozvoljen broj karaktera za primaoca je izmedju 4 i 120");
		return false;
    }
    else if(tezina < 0.1 || tezina > 9.99){
		alert("Tezina mora biti u opsegu 0.1 - 9.99")
		return false;
	}else if(cena < 250 || cena > 10000){
		alert("Cena mora biti veca od 250 a manja od 10000");
		return false;
	}
	return true;
};

function refreshTable() {
	
    loadPaketi();
};

function refresh() {
	
	document.getElementById("paketiForm").reset();
	
};

function submitSearchForm(){
	var najmanje = document.getElementById("najmanje").value;
	var najvise = document.getElementById("najvise").value; 
if(validateSearch()){

	sendData = {
		"Najmanje" : najmanje,
		"Najvise" : najvise
	};
var url = host + port + SearchEndpoint;	

console.log("Objekat za slanje");
console.log(sendData);


var headers = { 'Content-Type': 'application/json' };
if (jwt_token) {
	headers.Authorization = 'Bearer ' + jwt_token;		// headers.Authorization = 'Bearer ' + sessionStorage.getItem(data.token);
}

fetch(url, { method: "POST", headers : headers, body: JSON.stringify(sendData) })
	.then((response) => {
		if (response.status === 200) {
			response.json().then(setPaketi);
			document.getElementById("serchForm").reset();
		} else {
			console.log("Error occured with code " + response.status);
			alert("Error occured!");
		}
	})
	.catch(error => console.log(error));
return false;

}
	return false;
};

function validateSearch(){
	var najmanje = document.getElementById("najmanje").value;
	var najvise = document.getElementById("najvise").value; 

	if(najmanje < 0.1 || najmanje > 9.99){
		alert("Najmanja vrednost mora biti u opsegu 0.1 - 9.99");
		return false;
	}else if(najvise <= 0.1|| najvise > 9.99){
		alert("Najveca vrednost mora biti u opsegu 0.1 - 9.99");
		return false;
	}else if(najmanje > najvise){
        alert("Najmanja vrednost ne sme biti veca od najvece vrednosti.");
		return false;
    }
	return true;
};

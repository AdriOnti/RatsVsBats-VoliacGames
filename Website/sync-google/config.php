<?php

//start session on web page
session_start();

//config.php

//Include Google Client Library for PHP autoload file
require_once 'vendor/autoload.php';

//Make object of Google API Client for call Google API
$google_client = new Google_Client();

//Set the OAuth 2.0 Client ID | Copiar "ID DE CLIENTE"
$google_client->setClientId('242522315208-8iommf4528ao6679vu4m2dbq36a643je.apps.googleusercontent.com');

//Set the OAuth 2.0 Client Secret key
$google_client->setClientSecret('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');

//Set the OAuth 2.0 Redirect URI | URL AUTORIZADO
$google_client->setRedirectUri('https://rats-vs-bats-558152835.us-east-1.elb.amazonaws.com/index.php');

// to get the email and profile 
$google_client->addScope('email');

$google_client->addScope('profile');

?>

/// <reference path="../../assets/js/jquery.min.js" />
/// <reference path="../../assets/js/signalrjs/jquery.signalr-2.1.2.js" />
/// <reference path="../../assets/js/angular.js" />


(function () {
	var app = angular.module('chat-app', []);

	app.controller('ChatController', function ($scope, $window) {
		// scope variables
		$scope.name = 'Guest'; // holds the user's name
		$scope.message = ''; // holds the new message
		$scope.messages = []; // collection of messages coming from server
		$scope.chatHub = null; // holds the reference to hub
		$scope.connectionID = '';
		$scope.listUsers = [];
		$scope.chatHub = $.connection.chatHub; // initializes hub
		$scope.chatHub.client.usersConnected = function (list) {
			$scope.listUsers = list;
			$scope.$apply();
		};
		$.connection.hub.start().done(function () {
			$scope.chatHub.server.addUser("Đây là sesionuser");
			$window.alert("connection started");

		}).fail(function (e) {
			$window.alert(e);
		});
		// register a client method on hub to be invoked by the server
		$scope.chatHub.client.broadcastMessage = function (id, name, message) {
			var newMessage = { id: id, name: name, message: message };

			// push the newly coming message to the collection of messages
			$scope.messages.push(newMessage);
			$scope.$apply();
		};



		$scope.newMessage = function () {
			// sends a new message to the server
			$scope.chatHub.server.sendMessage($scope.name, $scope.message, $scope.connectionID);

			$scope.message = '';
		};
	});
}());
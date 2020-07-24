// This file is to show how a library package may provide JavaScript interop features
// wrapped in a .NET API

window.exampleJsFunctions = {
    showPrompt: function (message) {
        return prompt(message, 'Type anything here');
    }
};



window.SchoolozorDB = {
    Init: function () {
        window.indexedDB = window.indexedDB || window.mozIndexedDB || window.webkitIndexedDB ||
            window.msIndexedDB;

        window.IDBTransaction = window.IDBTransaction || window.webkitIDBTransaction ||
            window.msIDBTransaction;
        window.IDBKeyRange = window.IDBKeyRange ||
            window.webkitIDBKeyRange || window.msIDBKeyRange

        if (!window.indexedDB) {
            window.alert("Your browser doesn't support a stable version of IndexedDB.");
            return false;
        }

        let db;
        let request = window.indexedDB.open("SchoolozorDB", 1);

        request.onupgradeneeded = function (event) {
            db = event.target.result;
            window.location.reload();
        }
        return true;
    },
    Create: function (name) {
        let db;
        let request = window.indexedDB.open("SchoolozorDB", 2);

        request.onsuccess = function (event) {
            db = event.target.result;
        }
        
        request.onupgradeneeded = function (event) {
            db = event.target.result;
            let notes = db.createObjectStore(name, { autoIncrement: true });
        }
        
    },
    Insert: function (data) {
        let db;
        let request = window.indexedDB.open("SchoolozorDB");

        request.onsuccess = function (event) {
            db = event.target.result;
            let tx = db.transaction(['Notes'], 'readwrite');
            let store = tx.objectStore('Notes');

            for (var i = 0; i < data.length; i++) {
                store.add(data[i]);
            }
        }
    }
}
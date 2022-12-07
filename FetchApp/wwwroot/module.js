// Import the functions you need from the SDKs you need
import { initializeApp } from "https://www.gstatic.com/firebasejs/9.10.0/firebase-app.js";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
const firebaseConfig = {
    apiKey: "AIzaSyCDgJTNMuZgL3mTkR7UqzOONdEcS3xgQy4",
    authDomain: "poc-chris-project.firebaseapp.com",
    projectId: "poc-chris-project",
    storageBucket: "poc-chris-project.appspot.com",
    messagingSenderId: "998853939469",
    appId: "1:998853939469:web:e55cf643e30e55b1f52044"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);

import { getDatabase, ref, set, get, child, update, remove, onValue }
    from "https://www.gstatic.com/firebasejs/9.10.0/firebase-database.js"

const db = getDatabase();

(function () {
    window.db = {
        insert: function (userId, json) {
            set(ref(db, "CDO/" + userId), {
                USER_ID: userId,
                RAW: json
            })
                .then(() => {
                    window.db.read(userId);
                })
                .catch((error) => {
                    alert(error);
                });
        },
        read: function (userId) {
            const dbref = ref(db);
            get(child(dbref, "CDO/" + userId)).then((snapshot) => {
                if (snapshot.exists()) {
                    var value = snapshot.val().RAW;
                    localStorage['RAW'] = value;
                }
            });

        },
        init: function (userId) {
            var rawRef = ref(db, "CDO/" + userId);

            onValue(rawRef, (snapshot) => {
                console.log('change=============');
            }, {
                onlyOnce: true
            });
        }
    }
})();
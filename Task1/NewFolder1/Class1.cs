﻿
using Antlr.Runtime;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Printing;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using System.Xml.Linq;

using System;

< !DOCTYPE html >
< html lang = "en" >
< head >
    < meta charset = "UTF-8" >
    < meta name = "viewport" content = "width=device-width, initial-scale=1.0" >
    < title > Update Profile </ title >
    < style >
        body {
    font - family: Arial, sans - serif;
    background - image: url('https://static.vecteezy.com/system/resources/thumbnails/039/578/587/small_2x/ai-generated-flat-lay-composition-of-stylish-office-desk-photo.jpg');
    background - size: cover;
    background - repeat: no - repeat;
}

button {
            padding: 10px 15px;
margin: 5px 0;
background - color: navy;
color: aliceblue;
border: solid darkblue;
border - radius: 4px;
cursor: pointer;
font - weight: bolder;
width: 100 %;
font - size: larger;
        }

        form {
            margin: 20px;
padding: 20px;
        }

            form label
{
    display: block;
    margin-bottom: 5px;
    font-weight: bold;
    font-size: large;
    color:white;
}

form input, form button {
                margin-bottom: 15px;
width: 100 %;
padding: 8px;
font - size: 1rem;
            }

        #popup {
            position: fixed;
top: 50 %;
left: 50 %;
transform: translate(-50 %, -50 %);
color: green;
padding: 20px;
background - color: white;
border: 1px solid #ccc;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
display: none;
z - index: 1000;
text - align: center;
        }

            #popup button {
                margin - top: 10px;
padding: 10px 15px;
background - color: navy;
color: aliceblue;
border: solid darkblue;
border - radius: 4px;
cursor: pointer;
font - size: medium;
            }

        #overlay {
            position: fixed;
top: 0;
left: 0;
width: 100 %;
height: 100 %;
background - color: rgba(0, 0, 0, 0.5);
display: none;
z - index: 999;
        }

        #updateProfileForm > img {
            position: absolute;
margin - left: 400px;
margin - top: 45px;
        }
        #xyz{
            color: white;
        }
    </ style >
</ head >
< body >
    < h1 id = "xyz" > Update Profile </ h1 >
    < form id = "updateProfileForm" >

        < label for= "id" > User ID:</ label >
        < input type = "number" id = "id" placeholder = "Enter User ID" value = @ViewBag.Message readonly onblur= "fetchUserData()" />

        < label for= "userName" > Username:</ label >
        < input type = "text" id = "userName" placeholder = "Enter Username" required />

        < label for= "password" > Password:</ label >
        < input type = "password" id = "password" placeholder = "Enter Password" required />

        < label for= "email" > Email:</ label >
        < input type = "email" id = "email" placeholder = "Enter Email" required />

        < label for= "phoneNumber" > Phone Number:</ label >
        < input type = "text" id = "phoneNumber" placeholder = "Enter Phone Number" required />

        < !--Hidden Role ID Field-- >
        < input type = "hidden" id = "roleId" value = "2" />

        < button type = "button" onclick = "updateUserProfile()" > Update Profile </ button >
    </ form >

    < div id = "overlay" ></ div >
    < div id = "popup" >
        < p id = "popupMessage" ></ p >
        < button onclick = "closePopup()" > Close </ button >
    </ div >

    < script >
        const apiUrl = 'https://localhost:7071/api/Users'; // Ensure the URL matches your API route

// Display the popup message
function showPopup(message)
{
    const popupMessage = document.getElementById("popupMessage");
    const popup = document.getElementById("popup");
    const overlay = document.getElementById("overlay");

    popupMessage.textContent = message;
    popup.style.display = "block";
    overlay.style.display = "block";
}

// Close the popup
function closePopup()
{
    document.getElementById("popup").style.display = "none";
    document.getElementById("overlay").style.display = "none";
}

// Fetch user data by ID
function fetchUserData()
{
    const id = document.getElementById("id").value.trim();
    if (!id) return;

    fetch(`${ apiUrl}/${ id}`)
                .then(response => {
                     if (!response.ok)
                     {
                         throw new Error("User not found.");
                     }
                     return response.json();
                 })
                .then(data => {
                    document.getElementById("userName").value = data.userName || "";
                    document.getElementById("password").value = data.passwordHash || "";
                    document.getElementById("email").value = data.email || "";
                    document.getElementById("phoneNumber").value = data.phoneNumber || "";
                })
    .catch(error => {
                    showPopup(`Error: ${ error.message}`);
                });
}

// Update user profile
function updateUserProfile()
{
    const id = document.getElementById("id").value.trim();
    const userName = document.getElementById("userName").value.trim();
    const password = document.getElementById("password").value.trim();
    const email = document.getElementById("email").value.trim();
    const phoneNumber = document.getElementById("phoneNumber").value.trim();
    const roleId = document.getElementById("roleId").value.trim();

    if (!id || !userName || !password || !email || !phoneNumber)
    {
        return showPopup("All fields are required.");
    }

    const requestData = {
                email: email,
                phoneNumber: phoneNumber,
                passwordHash: password,
                userName: userName,
                roleId: parseInt(roleId, 10) // Ensure RoleId is an integer
            };

fetch(`${ apiUrl}/${ id}`, {
method: 'PUT',
                headers:
    {
        'Content-Type': 'application/json'
                },
                body: JSON.stringify(requestData)
            })
                .then(response => {
                     if (!response.ok)
                     {
                         return response.json().then(err => {
                             throw new Error(err.message || "Failed to update profile.");
                         });
                     }
                     return response.status === 204
                         ? { message: "Profile updated successfully." }
                        : response.json();
                 })
                .then(data => {
                    const message = data?.message || "Profile updated successfully!";
                    showPopup(message);
                })
                .catch(error => {
                    showPopup(`Error: ${ error.message}`);
                });
        }
    </ script >
</ body >
</ html >


// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


document.addEventListener("DOMContentLoaded", function () {
    const themeIcon = document.querySelector("#theme-icon");

    function updateIcon(theme) {
        themeIcon.className = theme === "light" ? "fas fa-sun" : "fas fa-moon";
    }

    function toggleTheme() {
        const currentTheme = document.documentElement.getAttribute("data-bs-theme");
        const newTheme = currentTheme === "light" ? "dark" : "light";

        document.documentElement.setAttribute("data-bs-theme", newTheme);
        localStorage.setItem("theme", newTheme);
        updateIcon(newTheme);
    }

    const savedTheme = localStorage.getItem("theme") || "light";
    document.documentElement.setAttribute("data-bs-theme", savedTheme);
    updateIcon(savedTheme);
    document.getElementById("theme-toggle").addEventListener("click", toggleTheme);
});

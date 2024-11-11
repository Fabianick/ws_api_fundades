// Cambia el favicon
(function () {
    var link = document.querySelector("link[rel*='icon']") || document.createElement('link');
    link.type = 'image/x-icon';
    link.rel = 'shortcut icon';
    link.href = '/swagger/favicon.ico';
    document.getElementsByTagName('head')[0].appendChild(link);
})();

/*
document.addEventListener("DOMContentLoaded", function () {
    var swaggerLogo = document.querySelector(".swagger-ui .topbar-wrapper .link img");
    if (swaggerLogo) {
        swaggerLogo.remove(); // Elimina el logo de Swagger si aún existe
    }

    var linkContainer = document.querySelector(".swagger-ui .topbar-wrapper .link");
    if (linkContainer) {
        var img = document.createElement("img");
        img.src = "https://fundades.org/wp-content/uploads/2020/09/logo-Fundades-web-2020.png";
        img.style.width = "200px";
        img.style.height = "auto";

        // Agrega el logo personalizado al contenedor
        linkContainer.appendChild(img);
    }
});
*/
(function () {
    var link = document.querySelector("link[rel*='icon']") || document.createElement('link');
    link.type = 'image/x-icon';
    link.rel = 'shortcut icon';
    link.href = '/swagger/favicon.ico'; // Ruta al favicon
    document.getElementsByTagName('head')[0].appendChild(link);
})();

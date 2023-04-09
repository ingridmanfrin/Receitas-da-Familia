$(function () {

    $('#side-menu').metisMenu();

});

//Loads the correct sidebar on window load,
//collapses the sidebar on window resize.
// Sets the min-height of #page-wrapper to window size
$(function () {
    $(window).on("load resize", function () {
        _handleMenu();
    });

    var url = window.location;
    var element = $('ul.nav a').filter(function () {
        return this.href == url || url.href.indexOf(this.href) == 0;
    }).addClass('active').parent().parent().addClass('in').parent();
    if (element.is('li')) {
        element.addClass('active');
    }

    _handleMenu();
});
function _handleMenu() {
    let navbar = $('div.navbar-collapse');
    if (!navbar.hasClass('navbar-collapse')) {
        setTimeout(_handleMenu, 100);
    }
    topOffset = 50;
    width = (this.window.innerWidth > 0) ? this.window.innerWidth : this.screen.width;
    if (width < 768) {
        navbar.removeClass('collapse in');
        navbar.addClass('collapse');
        topOffset = 100; // 2-row-menu
    } else {
        navbar.addClass('collapse in');
    }

    height = ((this.window.innerHeight > 0) ? this.window.innerHeight : this.screen.height) - 1;
    height = height - topOffset;
    if (height < 1) height = 1;
    if (height > topOffset) {
        $("#page-wrapper").css("min-height", (height) + "px");
    }
}

let deferredPrompt;
// const relatedApps = navigator.getInstalledRelatedApps();
// relatedApps.forEach((app) => {
//     console.log(app.id, app.platform, app.url);
// });


// window.addEventListener('beforeinstallprompt', (e) => {
//     // Prevent the mini-infobar from appearing on mobile
//     e.preventDefault();
//     // Stash the event so it can be triggered later.
//     deferredPrompt = e;
//     // Update UI notify the user they can install the PWA
//     // showInstallPromotion();
//     // Optionally, send analytics event that PWA install promo was shown.
//     console.log(`'beforeinstallprompt' event was fired.`);
// });

// window.addEventListener('appinstalled', () => {
//     // Hide the app-provided install promotion
//     hideInstallPromotion();
//     // Clear the deferredPrompt so it can be garbage collected
//     deferredPrompt = null;
//     // Optionally, send analytics event to indicate successful install
//     console.log('PWA was installed');
// });




// buttonInstall.addEventListener('click', async () => {
//     // Hide the app provided install promotion
//     hideInstallPromotion();
//     // Show the install prompt
//     deferredPrompt.prompt();
//     // Wait for the user to respond to the prompt
//     const { outcome } = await deferredPrompt.userChoice;
//     // Optionally, send analytics event with outcome of user choice
//     console.log(`User response to the install prompt: ${outcome}`);
//     // We've used the prompt, and can't use it again, throw it away
//     deferredPrompt = null;
// });

// async function getInstalledApps() {
//     const installedApps = await navigator.getInstalledRelatedApps();
//     if (installedApps.length === 0) {
//         deferredPrompt.prompt();
//         // Wait for the user to respond to the prompt
//         const { outcome } = await deferredPrompt.userChoice;
//         // Optionally, send analytics event with outcome of user choice
//         console.log(`User response to the install prompt: ${outcome}`);
//         // We've used the prompt, and can't use it again, throw it away
//         deferredPrompt = null;
//     }
// }
// setTimeout(() => {

//     if ('getInstalledRelatedApps' in navigator) {
//         getInstalledApps();
//     }

// }, 2000);
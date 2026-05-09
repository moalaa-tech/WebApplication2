// loader.js

function showLoader() {
    if (document.querySelector('.loader-overlay')) {
        return;
    }

    const overlay = document.createElement('div');
    overlay.className = 'loader-overlay';

    const loader = document.createElement('div');
    loader.className = 'loader';

    document.body.appendChild(overlay);
    document.body.appendChild(loader);
}

function hideLoader() {
    const overlay = document.querySelector('.loader-overlay');
    const loader = document.querySelector('.loader');

    if (overlay) {
        overlay.remove();
    }

    if (loader) {
        loader.remove();
    }
}

// toaster.js

function showToaster(message, type = 'info') {
    let container = document.querySelector('.toaster-container');
    if (!container) {
        container = document.createElement('div');
        container.className = 'toaster-container';
        document.body.appendChild(container);
    }

    const toaster = document.createElement('div');
    toaster.className = `toaster ${type}`;
    toaster.textContent = message;

    container.appendChild(toaster);

    setTimeout(() => {
        toaster.remove();
        if (container.childElementCount === 0) {
            container.remove();
        }
    }, 5000);
}

window.toaster = {
    success: (message) => showToaster(message, 'success'),
    error: (message) => showToaster(message, 'error'),
    info: (message) => showToaster(message, 'info'),
    warning: (message) => showToaster(message, 'warning'),
};

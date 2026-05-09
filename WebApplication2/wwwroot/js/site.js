// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

(function () {
    const html = document.documentElement;
    const rtlKey = 'crmapp-rtl';
    const rtlToggle = document.getElementById('rtlToggle');
    const bootstrapCss = document.getElementById('bootstrapCss');
    const bootstrapRtlCss = document.getElementById('bootstrapRtlCss');
    const cultureSelect = document.getElementById('cultureSelect');

    function applyRtl(isRtl) {
        if (!bootstrapCss || !bootstrapRtlCss) return;
        if (isRtl) {
            html.setAttribute('dir', 'rtl');
            bootstrapCss.disabled = true;
            bootstrapRtlCss.disabled = false;
        } else {
            html.setAttribute('dir', 'ltr');
            bootstrapCss.disabled = false;
            bootstrapRtlCss.disabled = true;
        }
        if (rtlToggle) {
            rtlToggle.classList.toggle('active', isRtl);
            rtlToggle.textContent = isRtl ? 'LTR' : 'RTL';
        }
    }

    // Initialize based on culture cookie if present; fallback to local storage
    function getCookie(name) {
        const match = document.cookie.match(new RegExp('(^| )' + name + '=([^;]+)'));
        return match ? decodeURIComponent(match[2]) : null;
    }
    const cultureCookie = getCookie('.AspNetCore.Culture');
    let startRtl = false;
    if (cultureCookie) {
        // cookie format c=%5Bculture%5D|uic=%5Bui-culture%5D
        const uiMatch = cultureCookie.match(/uic=([^|]+)/);
        const uiCulture = uiMatch ? uiMatch[1] : '';
        startRtl = uiCulture.toLowerCase().startsWith('ar');
    } else {
        const stored = localStorage.getItem(rtlKey);
        startRtl = stored === '1';
    }
    applyRtl(startRtl);

    if (rtlToggle) {
        rtlToggle.addEventListener('click', function () {
            const isRtl = html.getAttribute('dir') !== 'rtl';
            applyRtl(isRtl);
            localStorage.setItem(rtlKey, isRtl ? '1' : '0');
        });
    }

    if (cultureSelect) {
        cultureSelect.addEventListener('change', function () {
            const selected = cultureSelect.value;
            const willBeRtl = selected.toLowerCase().startsWith('ar');
            localStorage.setItem(rtlKey, willBeRtl ? '1' : '0');
        });
    }

    // Global localization pass for common buttons/links
    function localizeCommonLabels() {
        if (!window.__i18n) return;
        const map = window.__i18n;
        const selectors = [
            'a', 'button', 'input[type="submit"]', 'input[type="button"]'
        ];
        const elements = document.querySelectorAll(selectors.join(','));
        elements.forEach(el => {
            const original = (el.innerText || el.value || '').trim();
            if (!original) return;
            const key = Object.keys(map).find(k => original.toLowerCase() === k.toLowerCase());
            if (!key) return;
            const localized = map[key];
            if (!localized) return;
            if (el.tagName === 'INPUT') {
                el.value = localized;
            } else {
                el.innerText = localized;
            }
        });
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', localizeCommonLabels);
    } else {
        localizeCommonLabels();
    }
})();
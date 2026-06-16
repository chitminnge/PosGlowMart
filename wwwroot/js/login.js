const tabs = document.querySelectorAll('.tab');
const slider = document.getElementById('slider');
const formWrapper = document.getElementById('formWrapper');

tabs.forEach((tab, index) => {
    tab.addEventListener('click', () => {
        tabs.forEach(t => t.classList.remove('active'));
        tab.classList.add('active');

        formWrapper.style.transform = `translateX(-${index * 50}%)`;
        slider.style.transform = `translateX(${index * 100}%)`;
    });
});

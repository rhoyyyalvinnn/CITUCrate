function showSection(sectionId) {
    // Hide all sections
    const sections = document.querySelectorAll('.content-section');
    sections.forEach(section => section.style.display = 'none');

    // Show the selected section
    const selectedSection = document.getElementById(sectionId);
    if (selectedSection) {
        selectedSection.style.display = 'block';
    }

    // Update the active link
    document.querySelectorAll('.sidebar a').forEach(link => link.classList.remove('active'));
    document.querySelector(`.sidebar a[onclick="showSection('${sectionId}')"]`).classList.add('active');
}

// Set the default selection to "CIT-U Crate" on page load
document.addEventListener('DOMContentLoaded', () => {
    showSection('about');
});

// JavaScript to add 'active' class to the clicked link
document.querySelectorAll('.sidebar a').forEach(link => {
    link.addEventListener('click', function () {
        document.querySelectorAll('.sidebar a').forEach(item => item.classList.remove('active'));
        this.classList.add('active');
    });
});
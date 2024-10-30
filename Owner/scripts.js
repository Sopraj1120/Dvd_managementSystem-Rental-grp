document.addEventListener('DOMContentLoaded', function () {
    const shopsList = document.getElementById('shopsList');
    const addShopModal = document.getElementById('addShopModal');
    const addShopBtn = document.getElementById('addShopBtn');
    const closeModal = document.querySelector('.close');
    const addShopForm = document.getElementById('addShopForm');

    // Example data for shops (replace with actual data or use localStorage)
    let shops = [
        {
            location: 'Downtown',
            adminName: 'Alice Johnson',
            contact: '123-456-7890',
            email: 'alice@example.com',
            totalCustomers: 150,
            totalMovies: 50,
            activeRentals: 10,
            adminPage: 'admin1.html'
        },
        {
            location: 'Uptown',
            adminName: 'Bob Smith',
            contact: '987-654-3210',
            email: 'bob@example.com',
            totalCustomers: 200,
            totalMovies: 75,
            activeRentals: 20,
            adminPage: 'admin2.html'
        }
    ];

    // Function to render the shops
    function renderShops() {
        shopsList.innerHTML = '';
        shops.forEach((shop, index) => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${shop.location}</td>
                <td>${shop.adminName}</td>
                <td>${shop.contact}</td>
                <td>${shop.email}</td>
                <td>${shop.totalCustomers}</td>
                <td>${shop.totalMovies}</td>
                <td>${shop.activeRentals}</td>
                <td><button onclick="viewAdminPage('${shop.adminPage}')">View</button></td>
            `;
            shopsList.appendChild(row);
        });
    }

    // Function to handle "View" button and navigate to admin page
    window.viewAdminPage = function (adminPage) {
        window.location.href = adminPage;
    };

    // Show modal to add new shop
    addShopBtn.addEventListener('click', () => {
        addShopModal.style.display = 'block';
    });

    // Close the modal
    closeModal.addEventListener('click', () => {
        addShopModal.style.display = 'none';
    });

    // Add new shop
    addShopForm.addEventListener('submit', function (e) {
        e.preventDefault();
        const newShop = {
            location: document.getElementById('shopLocation').value,
            adminName: document.getElementById('adminName').value,
            contact: document.getElementById('adminContact').value,
            email: document.getElementById('adminEmail').value,
            totalCustomers: 0,  // Initially 0
            totalMovies: 0,     // Initially 0
            activeRentals: 0,   // Initially 0
            adminPage: `admin${shops.length + 1}.html` // Create new admin page link
        };
        shops.push(newShop);
        renderShops();
        addShopModal.style.display = 'none';
    });

    // Initial render of shops
    renderShops();
});

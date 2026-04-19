// ===== LOCAL STORAGE =====
function getCart() {
  return JSON.parse(localStorage.getItem("cart")) || [];
}

function saveCart(cart) {
  localStorage.setItem("cart", JSON.stringify(cart));
}

// ===== THÊM VÀO GIỎ =====
function addToCart(name, price, img) {
  let cart = getCart();

  let found = cart.find(item => item.name === name);

  if (found) {
    found.quantity++;
  } else {
    cart.push({
      name: name,
      price: price,
      img: img,
      quantity: 1
    });
  }

  saveCart(cart);
  alert("Đã thêm vào giỏ 🛒");
  renderCart(); // tự động cập nhật giỏ sau khi thêm
}

// ===== HIỂN THỊ GIỎ =====
function renderCart() {
  let cart = getCart();

  let container = document.getElementById("cart-items");
  let empty = document.getElementById("empty-cart");
  let totals = document.getElementById("cart-totals");

  if (!container) return;

  container.innerHTML = "";

  if (cart.length === 0) {
    empty.style.display = "block";       // hiện thông báo giỏ rỗng
    if (totals) totals.style.display = "none"; // ẩn subtotal & total
    return;
  } else {
    empty.style.display = "none";        // ẩn thông báo
    if (totals) totals.style.display = "block"; // hiện subtotal & total
  }

  let total = 0;

  cart.forEach((item, index) => {
    total += item.price * item.quantity;

    container.innerHTML += `
      <div class="cart-item">
        <img src="${item.img}" alt="${item.name}">
        <div class="cart-info">
          <h3>${item.name}</h3>
          <p>${item.price}đ</p>

          <div class="quantity">
            <button onclick="changeQty(${index}, -1)">-</button>
            <span>${item.quantity}</span>
            <button onclick="changeQty(${index}, 1)">+</button>
          </div>

          <a href="#" onclick="removeItem(${index})">Xóa</a>
        </div>
      </div>
    `;
  });

  document.getElementById("subtotal").innerText = total;
  document.getElementById("total").innerText = total + 30000; // + phí vận chuyển
}

// ===== TĂNG GIẢM SỐ LƯỢNG =====
function changeQty(index, amount) {
  let cart = getCart();

  cart[index].quantity += amount;

  if (cart[index].quantity <= 0) {
    cart.splice(index, 1);
  }

  saveCart(cart);
  renderCart();
}

// ===== XÓA SẢN PHẨM =====
function removeItem(index) {
  let cart = getCart();
  cart.splice(index, 1);
  saveCart(cart);
  renderCart();
}

// ===== LOAD TRANG =====
window.onload = function () {
  renderCart();
};
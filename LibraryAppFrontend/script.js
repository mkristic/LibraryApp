// @ts-nocheck

const books = JSON.parse(localStorage.getItem("books")) || [];;

const addForm = document.getElementById("addForm");
const tableBody = document.getElementById("tableBody");

const titleInput = document.getElementById("titleInput");
const authorInput = document.getElementById("authorInput");
const publicationYearInput = document.getElementById("publicationYearInput")

function saveToStorage() {
    localStorage.setItem("books", JSON.stringify(books));
}

addForm.addEventListener("submit", (e) => {
    e.preventDefault();
    const title = titleInput.value.trim();
    const author = authorInput.value.trim();
    const publicationYear = publicationYearInput.value.trim();

    const book = { id: Date.now(), title, author, year: publicationYear }; 
    
    books.push(book);
    saveToStorage();

    addForm.reset();
    showTable();
});

function deleteBook(id) {
    const bookIndex = books.findIndex(book => book.id === id);

    if (bookIndex !== -1) {
        books.splice(bookIndex);
    }        

    saveToStorage();
    showTable();
}

function editBook(id) {
    const book = books.find( book => book.id === id);

    if (!book) {
        return;
    }

    const row = document.querySelector(`tr[data-id="${id}"]`);

    row.cells[0].innerHTML = `<input class="edit-input" value="${book.title}">`;
    row.cells[1].innerHTML = `<input class="edit-input" value="${book.author}">`;
    row.cells[2].innerHTML = `<input class="edit-input" value="${book.publicationYear}">`;
    row.cells[3].innerHTML = `<button class="btn-confirm" onclick="confirmEdit(${id})">Confirm</button>`;
}

function confirmEdit(id) {
    const book = books.find(b => b.id === id);
    if (!book) return;
 
    const row = document.querySelector(`tr[data-id="${id}"]`);
    const inputs = row.querySelectorAll(".edit-input");
 
    book.title = inputs[0].value.trim();
    book.author = inputs[1].value.trim();
    book.year = inputs[2].value.trim();
 
    showTable();
}

function showTable() {
    tableBody.innerHTML = "";
 
    books.forEach(book => {
        const row = document.createElement("tr");
        row.setAttribute("data-id", book.id);
 
        row.innerHTML = `
            <td>${book.title}</td>
            <td>${book.author}</td>
            <td>${book.year}</td>
            <td>
                <button class="btn-edit" onclick="editBook(${book.id})">Edit</button>
                <button class="btn-delete" onclick="deleteBook(${book.id})">Delete</button>                
            </td>
        `;
 
        tableBody.appendChild(row);
    });
}

showTable();
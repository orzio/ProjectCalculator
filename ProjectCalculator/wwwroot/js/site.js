const uri = 'bending/';
let todos = [];

function getItems() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

function addItem() {

    const beam = {
        L1: parseFloat(document.getElementById('beam-l1').value),
        L2: parseFloat(document.getElementById('beam-l2').value),
        L3: parseFloat(document.getElementById('beam-l3').value),
        Q1: parseFloat(document.getElementById('beam-q1').value),
        Q2: parseFloat(document.getElementById('beam-q2').value)
    }

    const shape = {
        B1: parseFloat(document.getElementById('shape-b1').value),
        B2: parseFloat(document.getElementById('shape-b2').value),
        H1: parseFloat(document.getElementById('shape-h1').value),
        H2: parseFloat(document.getElementById('shape-h2').value)
    }      

    const yieldPoint = {
        kr: parseFloat(document.getElementById('task-kr').value)
    }

    const shapeType = parseInt(document.querySelector("input[name=shape]:checked").value);
    const beamType = parseInt(document.querySelector("input[name=beam]:checked").value);

    console.log(shapeType);
    console.log(beamType);

    const command = {
        BeamType: beamType,
        ShapeType: shapeType,
        Beam: beam,
        Shape: shape,
        YieldPoint: yieldPoint
    }

    console.log(command);

    console.log(uri);

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(command)
    })
        .then(response => response.json())
        //.then(() => {
        //    getItems();
        //    addNameTextbox.value = '';
        //})
        .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = todos.find(item => item.id === id);

    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-isComplete').checked = item.isComplete;
    document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(itemId, 10),
        isComplete: document.getElementById('edit-isComplete').checked,
        name: document.getElementById('edit-name').value.trim()
    };

    fetch(`${uri}/${itemId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'to-do' : 'to-dos';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    const tBody = document.getElementById('todos');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let isCompleteCheckbox = document.createElement('input');
        isCompleteCheckbox.type = 'checkbox';
        isCompleteCheckbox.disabled = true;
        isCompleteCheckbox.checked = item.isComplete;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(isCompleteCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.name);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    todos = data;
}
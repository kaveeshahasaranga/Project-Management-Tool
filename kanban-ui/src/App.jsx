import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { DragDropContext, Droppable, Draggable } from '@hello-pangea/dnd';

function App() {
  const [columns, setColumns] = useState([]);

  // Backend එකෙන් Data ගන්න Function එක
  const fetchData = () => {
    axios.get('http://localhost:5003/api/columns')
      .then((response) => {
        setColumns(response.data);
      })
      .catch((error) => console.error("Error:", error));
  };

  useEffect(() => {
    fetchData();
  }, []);

  // --- අලුත් කෑල්ල: කාඩ් එකක් ඇදලා අතඇරියම වෙන දේ ---
  const onDragEnd = (result) => {
    const { source, destination, draggableId } = result;

    // 1. එළියට ඇදලා දැම්මොත් හෝ තිබුණ තැනම දැම්මොත් මුකුත් කරන්නේ නෑ
    if (!destination) return;
    if (source.droppableId === destination.droppableId && source.index === destination.index) return;

    // 2. පරණ Columns ටික කොපි කරගන්නවා (React State එක කෙලින්ම වෙනස් කරන්න හොඳ නෑ)
    const newColumns = [...columns];
    
    // 3. කාඩ් එක ගත්ත Column එක සහ දාන Column එක හොයාගන්නවා
    const sourceCol = newColumns.find(col => col.id.toString() === source.droppableId);
    const destCol = newColumns.find(col => col.id.toString() === destination.droppableId);

    // 4. ඇදපු කාඩ් එක හොයාගන්නවා
    const draggedCard = sourceCol.cards.find(card => card.id.toString() === draggableId);

    // 5. පරණ තැනින් අයින් කරලා, අලුත් තැනට ඔබනවා
    sourceCol.cards.splice(source.index, 1);
    destCol.cards.splice(destination.index, 0, draggedCard);

    // 6. Screen එක Update කරනවා
    setColumns(newColumns);
  };

  // අලුත් කාඩ් එකක් හදන Function එක (පරණ එකමයි)
  const handleAddCard = (columnId) => {
    const title = window.prompt("මොකක්ද කරන්න තියෙන වැඩේ?");
    
    if (title) {
      const newCard = {
        title: title,
        description: "Added from UI",
        columnId: columnId
      };

      axios.post('http://localhost:5003/api/cards', newCard)
        .then(() => {
          fetchData(); 
        })
        .catch((error) => alert("වැඩේ අවුල්: " + error));
    }
  };

  return (
    <div style={{ padding: '20px', backgroundColor: '#f4f5f7', minHeight: '100vh', fontFamily: 'Arial, sans-serif' }}>
      <h1 style={{ textAlign: 'center', color: '#172b4d' }}>My Kanban Board 🚀</h1>
      
      {/* DragDropContext එකෙන් මුළු බෝඩ් එකම ආවරණය කරනවා */}
      <DragDropContext onDragEnd={onDragEnd}>
        <div style={{ display: 'flex', gap: '20px', justifyContent: 'center', alignItems: 'flex-start' }}>
          
          {columns.map((column) => (
            // Droppable: මේක ඇතුලට කාඩ් දාන්න පුළුවන්
            <Droppable key={column.id} droppableId={column.id.toString()}>
              {(provided) => (
                <div
                  ref={provided.innerRef}
                  {...provided.droppableProps}
                  style={{
                    backgroundColor: '#ebecf0',
                    padding: '15px',
                    borderRadius: '8px',
                    width: '300px',
                    boxShadow: '0 2px 5px rgba(0,0,0,0.1)',
                    minHeight: '100px' // කාඩ් නැති වුණත් පෙට්ටිය පේන්න ඕන නිසා
                  }}
                >
                  <h3 style={{ margin: '0 0 10px 0', color: '#172b4d' }}>{column.title}</h3>
                  
                  {/* කාඩ් ටික */}
                  {column.cards.map((card, index) => (
                    // Draggable: මේක අදින්න පුළුවන්
                    <Draggable key={card.id} draggableId={card.id.toString()} index={index}>
                      {(provided) => (
                        <div
                          ref={provided.innerRef}
                          {...provided.draggableProps}
                          {...provided.dragHandleProps}
                          style={{
                            backgroundColor: 'white',
                            padding: '12px',
                            marginBottom: '10px',
                            borderRadius: '5px',
                            boxShadow: '0 1px 3px rgba(0,0,0,0.2)',
                            ...provided.draggableProps.style
                          }}
                        >
                          {card.title}
                        </div>
                      )}
                    </Draggable>
                  ))}
                  {provided.placeholder} {/* හිස් ඉඩ වෙන් කරගන්න මේක ඕනෙමයි */}

                  {/* අලුත් කාඩ් දාන බට්න් එක */}
                  <button 
                    onClick={() => handleAddCard(column.id)}
                    style={{
                      width: '100%',
                      padding: '8px',
                      backgroundColor: 'transparent',
                      border: 'none',
                      color: '#5e6c84',
                      cursor: 'pointer',
                      textAlign: 'left',
                      borderRadius: '4px'
                    }}
                    onMouseOver={(e) => e.target.style.backgroundColor = 'rgba(9, 30, 66, 0.08)'}
                    onMouseOut={(e) => e.target.style.backgroundColor = 'transparent'}
                  >
                    + Add a card
                  </button>

                </div>
              )}
            </Droppable>
          ))}
          
        </div>
      </DragDropContext>
    </div>
  );
}

export default App;
import React, { useState, useEffect } from 'react';
import axios from 'axios';

function App() {
  const [columns, setColumns] = useState([]);

  // 1. Backend එකෙන් දත්ත ගේන Function එක
  const fetchData = () => {
    axios.get('http://localhost:5003/api/columns') // Port එක හරියට බලන්න
      .then((response) => {
        setColumns(response.data);
      })
      .catch((error) => console.error("Error:", error));
  };

  // React පටන් ගත්ත ගමන් දත්ත ගේන්න
  useEffect(() => {
    fetchData();
  }, []);

  // 2. අලුත් කාඩ් එකක් හදන Function එක
  const handleAddCard = (columnId) => {
    const title = window.prompt("මොකක්ද කරන්න තියෙන වැඩේ?"); // නම අහනවා
    
    if (title) {
      const newCard = {
        title: title,
        description: "Added from UI",
        columnId: columnId
      };

      // Backend එකට යවනවා
      axios.post('http://localhost:5003/api/cards', newCard)
        .then(() => {
          // හරි ගියොත් ආපහු ලිස්ට් එක අලුත් කරගන්නවා
          fetchData(); 
        })
        .catch((error) => alert("වැඩේ අවුල්: " + error));
    }
  };

  return (
    <div style={{ padding: '20px', backgroundColor: '#f4f5f7', minHeight: '100vh', fontFamily: 'Arial, sans-serif' }}>
      <h1 style={{ textAlign: 'center', color: '#172b4d' }}>My Kanban Board 🚀</h1>
      
      <div style={{ display: 'flex', gap: '20px', justifyContent: 'center', alignItems: 'flex-start' }}>
        
        {columns.map((column) => (
          <div key={column.id} style={{
            backgroundColor: '#ebecf0',
            padding: '15px',
            borderRadius: '8px',
            width: '300px',
            boxShadow: '0 2px 5px rgba(0,0,0,0.1)'
          }}>
            <h3 style={{ margin: '0 0 10px 0', color: '#172b4d' }}>{column.title}</h3>
            
            {/* කාඩ් ටික */}
            <div style={{ minHeight: '50px' }}>
              {column.cards && column.cards.map((card) => (
                <div key={card.id} style={{
                  backgroundColor: 'white',
                  padding: '12px',
                  marginBottom: '10px',
                  borderRadius: '5px',
                  boxShadow: '0 1px 3px rgba(0,0,0,0.2)',
                  cursor: 'grab'
                }}>
                  {card.title}
                </div>
              ))}
            </div>

            {/* අලුත් කාඩ් එකක් දාන බට්න් එක */}
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
        ))}

      </div>
    </div>
  );
}

export default App;
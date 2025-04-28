
const API_BASE = 'https://localhost:44345/api'; 

// ugyfelek letrehozasa
export const createUgyfel = async (ugyfelData) => {
    const response = await fetch(`${API_BASE}/Ugyfelek`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        Nev: ugyfelData.Nev,      
        Telefonszam: ugyfelData.Telefonszam,
        Email: ugyfelData.Email,
        Cim: ugyfelData.Cim
      })
    });
  if (!response.ok) throw new Error('Ügyfél létrehozása sikertelen');
  return response.json();
};

// idopont foglalas
export const createIdopont = async (idopontData) => {
  const response = await fetch(`${API_BASE}/IdopontFoglalasok`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
        UgyfelID: idopontData.ugyfelId,
        Datum: idopontData.datum,
        Megjegyzes: idopontData.megjegyzes
    })
  });
  if (!response.ok) throw new Error('Időpont foglalás sikertelen');
  return response.json();
};
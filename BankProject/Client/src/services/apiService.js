import axios from 'axios';

const api = axios.create({
  baseURL: 'https://mazebankapi-eehwdzcyaua6eca4.westeurope-01.azurewebsites.net/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

// ADD TOKEN TO HEADER
api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export const registerUser = async (username, email, password, role) => {
  return api.post('/auth/register', { username, email, password, role });
};

export const loginUser = async (username, password) => {
  return api.post('/auth/login', { username, password });
};


export const getBalance = async (accountId) => {
  return api.get(`/transaction/balance/${accountId}`);
};


export const getAccounts = async (userId) => {
  return api.get(`/account/${userId}`);
};

export const createAccount = async (userId) => {
  return api.get(`/account/create/${userId}`);
};

export const getTransactionsForAccount = async (accountId, filter) => {
  return api.post(`/transaction/all`, {accountId, filter});
};

export const deleteAccountByNumber = async (accountNumber) => {
  return api.delete(`/transaction/delete/${accountNumber}`);
};

export const deleteUserByName = async (username) => {
  return api.delete(`/admin/delete/${username}`);
};

export const deleteUserById = async (userId) => {
  return api.delete(`/request/delete/${userId}`);
};

export const getOperationDetail = async (operationId) => {
  return api.get(`/transaction/detail/${operationId}`);
}

export const getUsernameFromApi = async () => {
  return api.get('/request/username');
}

export const usernameReset = async (username, password) => {
  return api.post('/request/reset-username', {username, password});
}

export const passwordReset = async (oldPassword, newPassword) => {
  return api.post('/request/reset-password', {oldPassword, newPassword});
}

export const doTransfer = async (fromAccount, toAccount, amount) => {
  return api.post('/transaction/transfer', {fromAccount, toAccount, amount});
}

export const doDeposit = async (account, amount) => {
  return api.post('/transaction/deposit', { account, amount });
};

export const doDepositByName = async (account, amount) => {
  return api.post('/admin/deposit', { account, amount });
};

export const doWithdraw = async (account, amount) => {
  return api.post('/transaction/withdraw', {account, amount});
}

export const doWithdrawByName = async (account, amount) => {
  return api.post('/admin/withdraw', { account, amount });
};

export const getAccountNumberById = async (accountId) => {
  return api.get(`/request/account-number`, {
    params: { accountId }
  });
}

const getTokenPayload = () => {
  const token = localStorage.getItem('token');
  if (!token) return null;
  const payload = JSON.parse(atob(token.split('.')[1]));
  return payload;
}

export const getTokenId = () => {
  const payload = getTokenPayload();
  return payload ? payload.nameid : null;
}

export const getTokenRole = () => {
  const payload = getTokenPayload();
  return payload ? payload.role : null;
}


export const handleError = (errorMessage, error) => {
  const newError = {
    message: errorMessage,
    error: error.message
  };

  let errors = getErrorsFromLocalStorage();
  errors.push(newError);
  saveErrorsToLocalStorage(errors);

  throw new Error(error.message);
};

export const clearErrors = () => {
  localStorage.removeItem("errors");
};

export const getErrorsFromLocalStorage = () => {
  const storedErrors = localStorage.getItem("errors");
  return storedErrors ? JSON.parse(storedErrors) : [];
};

export const saveErrorsToLocalStorage = (errors) => {
  localStorage.setItem("errors", JSON.stringify(errors));
};

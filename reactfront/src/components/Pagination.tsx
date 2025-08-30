import React from 'react';
import { useDispatch } from 'react-redux';

const Pagination : React.FC<{pageIndex: number, setPageIndex: React.Dispatch<any>, totalPages: number, action: any}> = ({ pageIndex, setPageIndex, totalPages, action }) => {
  const pageNumbers = Array.from({ length: totalPages }, (_, index) => index + 1);
  const dispatch = useDispatch();
  
  const getItemProps = (index: number) =>
    ({
      variant: pageIndex === index ? "filled" : "text",
      color: "green",
      onClick: () => handlePageChange(index + 1)
    });

  const handlePageChange = (newPageIndex: number) => {
    setPageIndex(newPageIndex);
    dispatch(action(newPageIndex));
  };

  const prev = (e: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
    e.preventDefault();
    setPageIndex(pageIndex - 1);
    handlePageChange(pageIndex - 1);
  };

  const next = (e: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
    e.preventDefault();
    setPageIndex(pageIndex + 1);
    handlePageChange(pageIndex + 1);
  };

  return (
    <div className="flex items-center gap-4">
      <button
        className="flex items-center gap-2 cursor-pointer font-bold"
        onClick={prev}
        disabled={pageIndex === 1}
      >
        Previous
      </button>
      <div className="flex items-center gap-6">
        {pageNumbers.map((pageNumber) => (
            <p key={pageNumber} {...getItemProps(pageNumber)} onClick={() => handlePageChange(pageNumber)} className='hover:bg-green-800 font-bold border rounded-full w-9 h-9 cursor-pointer flex justify-center items-center'>
              {pageNumber}
            </p>
        ))}
      </div>
        <button
        className="flex items-center gap-2 cursor-pointer font-bold"
        onClick={next}
        disabled={pageIndex === totalPages}
        >
        Next
        </button>
    </div>
  );
};

export default Pagination;

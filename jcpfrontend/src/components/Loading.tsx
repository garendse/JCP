import hourglass from "../../images/icons/hourglass_empty_black_24dp.svg";
export function Loading() {
  return (
    <div className="flex justify-center">
      <div>
        <img src={hourglass} className="animate-spin block"></img>
      </div>
      <h1>Loading...</h1>
    </div>
  );
}

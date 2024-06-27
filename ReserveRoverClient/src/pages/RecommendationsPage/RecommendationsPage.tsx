/* eslint-disable jsx-a11y/anchor-is-valid */
import { FC, useEffect } from "react";
import styles from './RecommendationsPage.module.scss';
import PlaceCard from "../../components/place-card/place-card";
import { useAppDispatch, useAppSelector } from "../../hooks/redux-hooks";
import LoadingIndicator from "../../components/loading-indicator/LoadingIndicator";
import { useLocation } from "react-router-dom";
import { fetchRecommendedPlaces } from "../../redux/slices/placesSlice";

const RecommendationsPage: FC = function () {
  const { places: placesSelector, userInfo } = useAppSelector(state => state);
  const { status, error, places } = placesSelector;
  const { city } = userInfo;
  const dispatch = useAppDispatch();

  useLocation();

  useEffect(() => {
    dispatch(fetchRecommendedPlaces(city.id));
  }, [city])

  let content;
  if (status === 'failed') {
    content = <p>{error}</p>;
  } else if (status === 'succeeded') {
    content = places.map(place => <PlaceCard key={place.id} place={place} />)
  }

  return (
    <div className={`w-full bg-white ${styles["recommendations-page"]}`}>
      <div className="mb-5">
        <h3 className="text-3xl font-bold mb-1">
          Рекомендовано для вас
        </h3>
        <p className="text-base text-gray-500 dark:text-gray-300">
          Тут ви можете переглянути персоналізовані рекомендації закладів
        </p>
      </div>
      <div className={styles["cards-container"]}>
        {content}
      </div>
      {
        status === 'loading' &&
        <div className="flex items-center h-60">
          <LoadingIndicator />
        </div>
      }
    </div>
  );
};

export default RecommendationsPage;

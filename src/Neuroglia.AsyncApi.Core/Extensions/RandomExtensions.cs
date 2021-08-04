/*
 * Copyright © 2021 Neuroglia SPRL. All rights reserved.
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using System;

namespace Neuroglia.AsyncApi
{

    /// <summary>
    /// Defines extensions for <see cref="Random"/>s
    /// </summary>
    /// <remarks>Code taken from <see href="https://stackoverflow.com/a/28860710/3637555">Bryan Loeper</see></remarks>
    public static class RandomExtensions
    {

        /// <summary>
        /// Generates a new random decimal
        /// </summary>
        /// <param name="random">The extended <see cref="Random"/></param>
        /// <returns>A new random decimal</returns>
        public static decimal NextDecimal(this Random random)
        {
            if (random == null)
                throw new ArgumentNullException(nameof(random));
            return NextDecimal(random, decimal.MaxValue);
        }

        /// <summary>
        /// Generates a new random decimal
        /// </summary>
        /// <param name="random">The extended <see cref="Random"/></param>
        /// <param name="maxValue">The maximal decimal value</param>
        /// <returns>A new random decimal</returns>
        public static decimal NextDecimal(this Random random, decimal maxValue)
        {
            if (random == null)
                throw new ArgumentNullException(nameof(random));
            return NextDecimal(random, decimal.Zero, maxValue);
        }

        /// <summary>
        /// Generates a new random decimal
        /// </summary>
        /// <param name="random">The extended <see cref="Random"/></param>
        /// <param name="minValue">The minimal decimal value</param>
        /// <param name="maxValue">The maximal decimal value</param>
        /// <returns>A new random decimal</returns>
        public static decimal NextDecimal(this Random random, decimal minValue, decimal maxValue)
        {
            if (random == null)
                throw new ArgumentNullException(nameof(random));
            var nextDecimalSample = NextDecimalSample(random);
            return maxValue * nextDecimalSample + minValue * (1 - nextDecimalSample);
        }

        private static decimal NextDecimalSample(Random random)
        {
            if (random == null)
                throw new ArgumentNullException(nameof(random));
            decimal sample = 1m;
            while (sample >= 1)
            {
                int lo = random.Next();
                int mid = random.Next();
                int hi = random.Next(542101087);
                sample = new(lo, mid, hi, false, 28);
            }
            return sample;
        }

    }

}
